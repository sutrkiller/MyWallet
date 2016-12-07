﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyWallet.Models.Entries;
using MyWallet.Models.Home;
using MyWallet.Services.DataTransferModels;
using MyWallet.Services.Services.Interfaces;
using Newtonsoft.Json;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using JsonResult = System.Web.Mvc.JsonResult;
using SelectList = Microsoft.AspNetCore.Mvc.Rendering.SelectList;


namespace MyWallet.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class HomeController : Controller
    {
        private readonly IEntryService _entryService;
        private readonly IBudgetService _budgetService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public HomeController(IEntryService entryService, IMapper mapper, IBudgetService budgetService,
            IUserService userService)
        {
            _entryService = entryService;
            _mapper = mapper;
            _budgetService = budgetService;
            _userService = userService;
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Index()
        {
            var dashmodel = new DashboardModel();
            var newEntry = new CreateEntryViewModel();
            await FillSelectLists(newEntry);
            newEntry.EntryTime = DateTime.Now;
            dashmodel.Entry = newEntry;
            dashmodel.BudgetGraph = await PrepareGraphViewModel();
            return View(dashmodel);
        }

        private async Task FillSelectLists(CreateEntryViewModel newEntry)
        {
            var categories = await _budgetService.GetAllCategories();
            var categoriesList = categories.Select(d => new { Id = d.Id, Value = d.Name });
            newEntry.CategoriesList = new SelectList(categoriesList, "Id", "Value");
            var budgets = await _budgetService.GetAllBudgets();
            var budgetsList = budgets.Select(g => new { g.Id, Value = g.Name });
            newEntry.BudgetsList = new SelectList(budgetsList, "Id", "Value");
            var currencies = await _entryService.GetAllCurrencies();
            var currenciesList = currencies.Select(g => new { g.Id, Value = g.Code });
            var currenciesList2 = currencies.Select(g => new { g.Id, Value = g.Code });
            newEntry.CurrenciesList = new SelectList(currenciesList, "Id", "Value");
            newEntry.CustomCurrenciesList = new SelectList(currenciesList2, "Id", "Value");
            var prefCurrency =(await _userService.EnsureUserExists(User.Identity as ClaimsIdentity)).PreferredCurrency.Id;
            newEntry.CurrencyId = prefCurrency;


        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        public IActionResult EasterEgg()
        {
            return View();
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        public IActionResult Graphs()
        {
            return View();
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        public IActionResult Stats()
        {
            return View();
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create(CreateEntryViewModel entry)
        {
            var dashboard = new DashboardModel();
            await FillSelectLists(entry);
            dashboard.Entry = entry;

            dashboard.BudgetGraph = await PrepareGraphViewModel();

            if (ModelState.IsValid)
            {
                try
                {
                    var email = User.FindFirst(ClaimTypes.Email)?.Value;
                    entry.Amount = entry.IsIncome == true ? entry.Amount : -1*entry.Amount;
                    var conratios = await _entryService.GetConversionRatiosForCurrency(entry.CurrencyId);
                    var date = conratios.Max(cr => cr.Date);
                    entry.ConversionRatioId = conratios.FirstOrDefault(x => x.Date == date).Id;

                    entry.EntryTime = DateTime.Now;
                    entry.CategoryIds = new List<Guid>();
                    await
                        _entryService.AddEntry(_mapper.Map<EntryDTO>(entry), email, entry.ConversionRatioId,
                            entry.CategoryIds, entry.BudgetIds);
                    var currencyCode = entry.CurrenciesList.FirstOrDefault(x => Guid.Parse(x.Value) == entry.CurrencyId).Text;

                    TempData["MessageTitle"] = "Entry";
                    TempData["Message"] = $"{entry.Description}: {entry.Amount} {currencyCode} from {entry.EntryTime} was added.";
                  
                    return RedirectToAction("Index");
                }
                catch (NullReferenceException ex)
                {
                    Debug.WriteLine(ex.Message);
                }

            }
    
            return View("Index", dashboard);
        }

        private async Task<GraphViewModel> PrepareGraphViewModel()
        {
            var budget = await _budgetService.GetLastUsedBudget();

            var incomes = budget.Entries.Where(x=>x.Amount > 0 && x.EntryTime <= budget.EndDate && x.EntryTime >= budget.StartDate).GroupBy(x => x.EntryTime.Date).OrderBy(x => x.Key);
            var expenses = budget.Entries.Where(x=>x.Amount < 0 && x.EntryTime <= budget.EndDate && x.EntryTime >= budget.StartDate).GroupBy(x => x.EntryTime.Date).OrderBy(x => x.Key);

            var entries = budget.Entries.Where(x=>x.EntryTime<= budget.EndDate && x.EntryTime >= budget.StartDate).GroupBy(x => x.EntryTime.Date).OrderBy(x=>x.Key);
            var model =  new GraphViewModel()
            {
                GraphTitle = budget.Name,
                BudgetTitle = "Budget",
                DateTitle = "Dates",
                EntriesTitle = "Remaining budget"
            };

            model.Budget = budget.Amount;
            
            model.Labels = Enumerable.Range(0, budget.EndDate.Subtract(budget.StartDate).Days + 1)
                     .Select(d => budget.StartDate.AddDays(d).Date.ToString("O")).ToList();

            var tmpEntries =
                entries.Select(
                    x => new {
                        Sum = x.Sum(e =>
                   decimal.Divide(decimal.Multiply(e.Amount, e.ConversionRatio.Ratio),
                       budget.ConversionRatio.Ratio)),
                        Date = x.Key
                    }).ToList();

            var tmpIncomes = incomes.Select(
                    x => new {
                        Sum = x.Sum(e =>
                   decimal.Divide(decimal.Multiply(e.Amount, e.ConversionRatio.Ratio),
                       budget.ConversionRatio.Ratio)),
                        Date = x.Key
                    }).ToList();

            var tmpExpenses = expenses.Select(
                   x => new {
                       Sum = x.Sum(e =>
                  decimal.Divide(decimal.Multiply(e.Amount, e.ConversionRatio.Ratio),
                      budget.ConversionRatio.Ratio)),
                       Date = x.Key
                   }).ToList();

            List<decimal> values = new List<decimal>();
            List<decimal> valuesIn = new List<decimal>();
            List<decimal> valuesEx = new List<decimal>();
            foreach (var label in model.Labels)
            {

                valuesIn.Add(tmpIncomes.FirstOrDefault(e => e.Date.Date.ToString("O") == label)?.Sum ??  0m);
                valuesEx.Add(tmpExpenses.FirstOrDefault(e => e.Date.Date.ToString("O") == label)?.Sum ??  0m);
                values.Add(valuesIn.Sum() + valuesEx.Sum());
                //values.Add(tmpEntries.FirstOrDefault(e => e.Date.Date.ToString("O") == label)?.Sum + values.Sum() ?? (values.Any() ? values.Last() : 0m));
            }
            model.Entries = values;
            model.Incomes = valuesIn;
            model.Expenses = valuesEx;

            return model;
        }

        public IActionResult AddRatio()
        {
            return View();
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetChart()
        {
            var lastBudgetId = await _budgetService.GetLastUsedBudget();
            var data = lastBudgetId.Entries.Select(e => new
            {
                Date = e.EntryTime.ToShortDateString(),
                Budget = lastBudgetId.Amount,
                Entry =
                decimal.Divide(decimal.Multiply(e.Amount, e.ConversionRatio.Ratio), lastBudgetId.ConversionRatio.Ratio)
                    .ToString("F2")
            });
            var tmp =Enumerable.Range(1, 20).Select(e => new {Date = e, Budget = 100, Entry = e});
            //return Content(JsonConvert.SerializeObject(tmp), "application/json");
            return Json(tmp);
        }
    }

}
