using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWallet.Helpers;
using MyWallet.Models.Graphs;
using MyWallet.Models.Home;
using MyWallet.Services.DataTransferModels;
using MyWallet.Services.Filters;
using MyWallet.Services.Services.Interfaces;

namespace MyWallet.Controllers
{
    [Authorize]
    public class GraphsController : Controller
    {
        private readonly IEntryService _entryService;
        private readonly IBudgetService _budgetService;
        private readonly IUserService _userService;

        public GraphsController(IEntryService entryService, IBudgetService budgetService, IUserService userService)
        {
            _entryService = entryService;
            _budgetService = budgetService;
            _userService = userService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var model = new GraphsViewModel();
            model.BudgetGraphModel = await PrepareLastBudgetGraphViewModel();
            model.BudgetGraphCategoriesModel = await PrepareLastBudgetByCategoriesGraphViewModel();
            model.EntriesGraphModel = PrepareEntriesGraphViewModel();

            return View(model);
        }

        private GraphViewModel PrepareEntriesGraphViewModel()
        {
            return new GraphViewModel
            {
                GraphTitle = "Entries",
                ColumnTitles = new List<string> { "Dates","Incomes","Expenses","Total"}
            };
        }

        internal async Task<GraphViewModel> PrepareLastBudgetGraphViewModel()
        {
            var budget = await _budgetService.GetLastUsedBudget();
            var allBudgets = (await _budgetService.GetAllBudgets()).Select(x => new { Id = x.Id, Value = x.Name });
            return new GraphViewModel
            {
                GraphTitle = $"{budget.Name} in {budget.ConversionRatio.CurrencyFrom.Code}",
                ColumnTitles = new List<string> { "Dates", "Incomes", "Expenses", "Remaining budget" },
                BudgetId = budget.Id,
                Budgets = new SelectList(allBudgets, "Id", "Value")
            };
        }

        private async Task<GraphViewModel> PrepareLastBudgetByCategoriesGraphViewModel()
        {
            var budget = await _budgetService.GetLastUsedBudget();
            var allBudgets = (await _budgetService.GetAllBudgets()).Select(x => new { Id = x.Id, Value = x.Name });
            return new GraphViewModel
            {
                GraphTitle = $"{budget.Name} in {budget.ConversionRatio.CurrencyFrom.Code}",
                ColumnTitles = new List<string> { "Categories", "Total balance" },
                BudgetId = budget.Id,
                Budgets = new SelectList(allBudgets, "Id", "Value")
            };
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetDonutChartData(Guid? id)
        {
            BudgetDTO budget;
            if (id != null)
            {
                budget = await _budgetService.GetBudget(id.Value);
            }
            else
            {
                budget = await _budgetService.GetLastUsedBudget();
            }


            var labels = budget.Entries.SelectMany(x => x.Categories.Select(c => c.Name)).Distinct().ToList();

            var groups = labels.ToDictionary(label => label, label => budget.Entries.Where(x => x.Categories.Any(c => c.Name == label)).ToList());
            groups.Add("No category", budget.Entries.Where(x => !x.Categories.Any()).ToList());

            return Json(new
            {
                Currency = budget.ConversionRatio.CurrencyFrom.Code,
                Data = groups.Select(
                x =>
                    new
                    {
                        Label = x.Key,
                        Income = x.Value.Where(v => v.Amount >= 0)
                            .Select(v => Math.Round(v.ToBudgetCurrency(budget.ConversionRatio.Ratio),2))
                            .Sum(),
                        Expense = x.Value.Where(v => v.Amount < 0)
                            .Select(v => Math.Round(v.ToBudgetCurrency(budget.ConversionRatio.Ratio),2))
                            .Sum()
                    })
            });
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetBudgetChartData(Guid? id)
        {
            BudgetDTO budget;
            if (id != null)
            {
                budget = await _budgetService.GetBudget(id.Value);
            }
            else
            {
                budget = await _budgetService.GetLastUsedBudget();
            }

            var incomes =
                budget.Entries.Where(
                        x => x.Amount > 0 && x.EntryTime <= budget.EndDate && x.EntryTime >= budget.StartDate)
                    .GroupBy(x => x.EntryTime.Date)
                    .OrderBy(x => x.Key);
            var expenses =
                budget.Entries.Where(
                        x => x.Amount < 0 && x.EntryTime <= budget.EndDate && x.EntryTime >= budget.StartDate)
                    .GroupBy(x => x.EntryTime.Date)
                    .OrderBy(x => x.Key);

            var labels = Enumerable.Range(0, budget.EndDate.Subtract(budget.StartDate).Days + 1)
                .Select(d => budget.StartDate.AddDays(d).Date.ToString("O")).ToList();

            var tmpIncomes = incomes.Select(
                x => new
                {
                    Sum = x.Sum(e => e.ToBudgetCurrency(budget.ConversionRatio.Ratio)),
                    Date = x.Key
                }).ToList();

            var tmpExpenses = expenses.Select(
                x => new
                {
                    Sum = x.Sum(e => e.ToBudgetCurrency(budget.ConversionRatio.Ratio)),
                    Date = x.Key
                }).ToList();

            List<decimal> values = new List<decimal>();
            List<decimal> valuesIn = new List<decimal>();
            List<decimal> valuesEx = new List<decimal>();
            foreach (var label in labels)
            {

                valuesIn.Add(tmpIncomes.FirstOrDefault(e => e.Date.Date.ToString("O") == label)?.Sum ?? 0m);
                valuesEx.Add(tmpExpenses.FirstOrDefault(e => e.Date.Date.ToString("O") == label)?.Sum ?? 0m);
                values.Add(budget.Amount + valuesIn.Sum() + valuesEx.Sum());
            }

            return Json(new
            {
                Currency = budget.ConversionRatio.CurrencyFrom.Code,
                Data = Enumerable.Range(0, values.Count)
                    .Select(x => new { Label = labels[x], Income = Math.Round(valuesIn[x],2), Expense = Math.Round(valuesEx[x],2), Value = Math.Round(values[x],2) })
            });
        }

        public async Task<IActionResult> GetEntriesChartData()
        {
            DateTime dateFrom;
            DateTime dateTo = DateTime.Today;
            var user = await _userService.EnsureUserExists(User.Identity as ClaimsIdentity);
            var entries = (await _entryService.GetAllEntries(new EntriesFilter() {UserId = user.Id})).GroupBy(x=>x.EntryTime.Date).OrderBy(x=>x.Key);
            var crs = await _entryService.GetConversionRatiosForCurrency(user.PreferredCurrency.Id);
            var prefCr = crs.OrderByDescending(x => x.Date).FirstOrDefault();
            //if (!entries.Any()) return Json(null);

            var first = entries.FirstOrDefault();
            dateFrom = first.Key;
            if (first != null)
            {
                var labels = Enumerable.Range(0, DateTime.Today.Subtract(dateFrom).Days + 1)
                    .Select(d => first.Key.AddDays(d).Date.ToString("O")).ToList();

               // var datedEntries = new Dictionary<string, List<EntryDTO>>();



                var datedEntries = labels.Select(x => new {Label = x, Entries = entries.SingleOrDefault(e => e.Key.Date.ToString("O") == x)?.ToList() ?? new List<EntryDTO>()}).ToList();

                return Json(new
                {
                    Currency = user.PreferredCurrency.Code,
                    DateRange = $"{dateFrom:MM/dd/yyyy} - {dateTo:MM/dd/yyyy}",
                    Data =
                    datedEntries.Select(
                        (x,i) =>
                            new
                            {
                                x.Label,
                                Income =
                                Math.Round(x.Entries.Where(e => e.Amount > 0).Sum(e => e.ToBudgetCurrency(prefCr?.Ratio ?? 1m)),2),
                                Expense =
                                Math.Round(x.Entries.Where(e => e.Amount < 0).Sum(e => e.ToBudgetCurrency(prefCr?.Ratio ?? 1m)),2),
                                Balance = Math.Round(datedEntries.Take(i+1).SelectMany(e=>e.Entries).Sum(e=>e.ToBudgetCurrency(prefCr?.Ratio ?? 1m)),2)
                            })
                });
            }

            return Json(new
            {
                Currency = user.PreferredCurrency.Code,
                Data = new List<object>()
            });
        }

    }
}
