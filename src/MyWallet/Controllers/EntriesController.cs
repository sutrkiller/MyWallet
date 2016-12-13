using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWallet.Models.Entries;
using MyWallet.Services.DataTransferModels;
using MyWallet.Services.Filters;
using MyWallet.Services.Services.Interfaces;
using Sakura.AspNetCore;

namespace MyWallet.Controllers
{
    public class EntriesController : Controller
    {
        private readonly IEntryService _entryService;
        private readonly IBudgetService _budgetService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private const string MaxGuid = "ffffffff-ffff-ffff-ffff-ffffffffffff";
        public EntriesController(IEntryService entryService, IMapper mapper, IBudgetService budgetService,
            IUserService userService)
        {
            _entryService = entryService;
            _mapper = mapper;
            _budgetService = budgetService;
            _userService = userService;
        }

        private const int PageSize = 10;
        // GET: Budgets
        [Authorize]
        public async Task<IActionResult> List(DateTime? from = null, DateTime? to =null,int? page=null)
        {
            var userId = await _userService.GetUserId(User.Identity as ClaimsIdentity);
            var entries = await _entryService.GetAllEntries(new EntriesFilter() {From = from,To = to,UserId = userId});
            ViewData["from"] = from?.ToString("MM/dd/yyyy");
            ViewData["to"] = to?.ToString("MM/dd/yyyy");
            int pageNumber = page ?? 1;
            return View("List",_mapper.Map<IEnumerable<EntryViewModel>>(entries.OrderByDescending(x=>x.EntryTime)).ToPagedList(PageSize,pageNumber));
        }

        [Authorize]
        public async Task<IActionResult> Edit(Guid id)
        {
            var entryDto = await _entryService.GetEntry(id);
            if (entryDto == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<EditEntryViewModel>(entryDto);
            model.IsIncome = model.Amount  > 0;
            model.Amount = Math.Abs(Convert.ToDecimal(model.Amount));
            await FillSelectLists(model);
            return View(model);
        }

        // GET: Budgets/Details/[id]
        [Authorize]
        public async Task<IActionResult> Details(Guid id)
        {
            var entryDto = await _entryService.GetEntry(id);
            if (entryDto == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<EntryDetailsViewModel>(entryDto);
            return View(model);
        }

        // GET: Budgets/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var newEntry = new CreateEntryViewModel();
            await FillSelectLists(newEntry);
            newEntry.EntryTime = DateTime.Now;
            return View(newEntry);
        }

        private async Task FillSelectLists(CreateEntryViewModel newEntry)
        {
            var userId = await _userService.GetUserId(User.Identity as ClaimsIdentity);
            var budgets = new BudgetDTO[0];
            if (userId != null)
            {
                budgets = await _budgetService.GetAllBudgets(new BudgetFilter { UserId = userId });
            }
            var categories = await _budgetService.GetAllCategories();
            var categoriesList = categories.Select(d => new {Id = d.Id, Value = d.Name});
            newEntry.CategoriesList = new SelectList(categoriesList, "Id", "Value");
            var budgetsList = budgets.Select(g => new {g.Id, Value = g.Name});
            newEntry.BudgetsList = new SelectList(budgetsList, "Id", "Value");
            var currencies = await _entryService.GetAllCurrencies();
            var currenciesList = currencies.Select(g => new {g.Id, Value = g.Code});
            var currenciesList2 = currencies.Select(g => new {g.Id, Value = g.Code});
            newEntry.CurrenciesList = new SelectList(currenciesList, "Id", "Value");
            newEntry.CustomCurrenciesList = new SelectList(currenciesList2, "Id", "Value");
            var prefCurrency = (await _userService.EnsureUserExists(User.Identity as ClaimsIdentity)).PreferredCurrency.Id;
            newEntry.CurrencyId = prefCurrency;
            var conversionRatios = await _entryService.GetConversionRatiosForCurrency(prefCurrency);
            newEntry.ConversionRatiosList = FormatConversionRatioForSelectList(conversionRatios);
        }

        private async Task FillSelectLists(EditEntryViewModel newEntry)
        {
            var userId = await _userService.GetUserId(User.Identity as ClaimsIdentity);
            var budgets = new BudgetDTO[0];
            if (userId != null)
            {
                budgets = await _budgetService.GetAllBudgets(new BudgetFilter { UserId = userId });
            }
            var categories = await _budgetService.GetAllCategories();
            var categoriesList = categories.Select(d => new { Id = d.Id, Value = d.Name });
            newEntry.CategoriesList = new SelectList(categoriesList, "Id", "Value");
            var budgetsList = budgets.Select(g => new { g.Id, Value = g.Name });
            newEntry.BudgetsList = new SelectList(budgetsList, "Id", "Value");
            var currencies = await _entryService.GetAllCurrencies();
            var currenciesList = currencies.Select(g => new { g.Id, Value = g.Code });
            var currenciesList2 = currencies.Select(g => new { g.Id, Value = g.Code });
            newEntry.CurrenciesList = new SelectList(currenciesList, "Id", "Value");
            newEntry.CustomCurrenciesList = new SelectList(currenciesList2, "Id", "Value");
            var conversionRatios = await _entryService.GetConversionRatiosForCurrency(newEntry.CurrencyId);
            newEntry.ConversionRatiosList = FormatConversionRatioForSelectList(conversionRatios);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEntryViewModel entry)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var email = User.FindFirst(ClaimTypes.Email)?.Value;
                    entry.Amount = entry.IsIncome == true ? entry.Amount : -1*entry.Amount;
                    if(entry.ConversionRatioId.ToString() == MaxGuid)
                    { 
                        var customRatio = await _entryService.AddConversionRatio(entry.CurrencyId,entry.CustomRatioAmount,entry.CustomRatioCurrencyId);
                        entry.ConversionRatioId = customRatio.Id;
                    }
                    await _entryService.AddEntry(_mapper.Map<EntryDTO>(entry), email, entry.ConversionRatioId, entry.CategoryIds, entry.BudgetIds);
                    return RedirectToAction("List");
                }
                catch (NullReferenceException ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                
            }
            await FillSelectLists(entry);
            return View(entry);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditEntryViewModel entry)
        {
            if (ModelState.IsValid)
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                entry.Amount = entry.IsIncome == true ? entry.Amount : -1 * entry.Amount;
                if (entry.ConversionRatioId.ToString() == MaxGuid)
                {
                    var customRatio = await _entryService.AddConversionRatio(entry.CurrencyId, entry.CustomRatioAmount, entry.CustomRatioCurrencyId);
                    entry.ConversionRatioId = customRatio.Id;
                }
                await _entryService.EditEntry(_mapper.Map<EntryDTO>(entry), email, entry.ConversionRatioId, entry.CategoryIds, entry.BudgetIds);
                return RedirectToAction("List");
            }
            await FillSelectLists(entry);
            return View(entry);
        }

        [HttpGet]
        public async Task<IActionResult> GetConversionRatiosByCurrencyId(string currencyId)
        {
            Guid id;
            bool isValid = Guid.TryParse(currencyId, out id);
            
            if (!isValid || string.IsNullOrEmpty(currencyId))
            {
                var conversionRatiosEr = await  _entryService.GetAllConversionRatios();
                var resultEr = FormatConversionRatioForSelectList(conversionRatiosEr);
                return Json(resultEr);
            }

            var conversionRatios = await _entryService.GetConversionRatiosForCurrency(id);
            var result = FormatConversionRatioForSelectList(conversionRatios);
            return Json(result);
        }

        private static SelectList FormatConversionRatioForSelectList(IEnumerable<ConversionRatioDTO> conversionRatios)
        {
            var result = conversionRatios.OrderByDescending(x=>x.Date).Select(
                g => new {g.Id, Value = g.CurrencyFrom.Code + " - " + g.CurrencyTo.Code + " - " + g.Ratio});
            return  new SelectList(result, "Id", "Value").Add("Custom", MaxGuid, SelectListHelper.ListPosition.Last);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _entryService.DeleteEntry(id);
            return RedirectToAction("List");
        }
        
    }
    public static class SelectListHelper
    {
        public static SelectList Add(this SelectList list, string text, string value = "", ListPosition listPosition = ListPosition.First)
        {
            if (string.IsNullOrEmpty(value))
            {
                value = text;
            }
            var listItems = list.ToList();
            var lp = (int)listPosition;
            switch (lp)
            {
                case -1:
                    lp = list.Count();
                    break;
                case -2:
                    lp = list.Count() / 2;
                    break;
                case -3:
                    var random = new Random();
                    lp = random.Next(0, list.Count());
                    break;
            }
            listItems.Insert(lp, new SelectListItem { Value = value, Text = text});
            list = new SelectList(listItems, "Value", "Text");
            return list;
        }

        public enum ListPosition
        {
            First = 0,
            Last = -1,
            Middle = -2,
            Random = -3
        }
    }
}
