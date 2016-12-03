using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Claims;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWallet.Models.Entries;
using MyWallet.Services.DataTransferModels;
using MyWallet.Services.Services.Interfaces;

namespace MyWallet.Controllers
{
    public class EntriesController : Controller
    {
        private readonly IEntryService _entryService;
        private readonly IBudgetService _budgetService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public EntriesController(IEntryService entryService, IMapper mapper, IBudgetService budgetService,
            IUserService userService)
        {
            _entryService = entryService;
            _mapper = mapper;
            _budgetService = budgetService;
            _userService = userService;
        }

        // GET: Budgets
        [Authorize]
        public async Task<IActionResult> List()
        {
            var entriesDTO = await _entryService.GetAllEntries();
            return View(_mapper.Map<IEnumerable<EntryViewModel>>(entriesDTO));
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

            var categories = await _budgetService.GetAllCategories();
            var categoriesList = categories.Select(d => new { Id = d.Id, Value = d.Name });
            model.CategoriesList = new SelectList(categoriesList, "Id", "Value");
            var budgets = await _budgetService.GetAllBudgets();
            var budgetsList = budgets.Select(g => new { g.Id, Value = g.Name });
            model.BudgetsList = new SelectList(budgetsList, "Id", "Value");
            var currencies = await _entryService.GetAllCurrencies();
            var currenciesList = currencies.Select(g => new { g.Id, Value = g.Code });
            model.CurrenciesList = new SelectList(currenciesList, "Id", "Value");
            var onversionRatios = await _entryService.GetAllConversionRatios();
            var onversionRatiosList =
                onversionRatios.Select(
                    g => new { g.Id, Value = g.CurrencyFrom.Code + " - " + g.CurrencyTo.Code + " - " + g.Ratio });
            model.ConversionRatiosList = new SelectList(onversionRatiosList, "Id", "Value");
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
            var categories = await _budgetService.GetAllCategories();
            var categoriesList = categories.Select(d => new {Id = d.Id, Value = d.Name});
            newEntry.CategoriesList = new SelectList(categoriesList, "Id", "Value");
            var budgets = await _budgetService.GetAllBudgets();
            var budgetsList = budgets.Select(g => new {g.Id, Value = g.Name});
            newEntry.BudgetsList = new SelectList(budgetsList, "Id", "Value");
            var currencies = await _entryService.GetAllCurrencies();
            var currenciesList = currencies.Select(g => new {g.Id, Value = g.Code});           
            var currenciesList2 = currencies.Select(g => new { g.Id, Value = g.Code });            
            newEntry.CurrenciesList = new SelectList(currenciesList, "Id", "Value");
            newEntry.CustomCurrenciesList = new SelectList(currenciesList2, "Id", "Value");
            var conversionRatios = await _entryService.GetConversionRatiosForCurrency(currencies.FirstOrDefault().Id);
            newEntry.ConversionRatiosList = FormatConversionRatioForSelectList(conversionRatios);
            newEntry.EntryTime = DateTime.Now;
            return View(newEntry);
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
                    entry.Amount = entry.IsIncome == true ? entry.Amount : "-" + entry.Amount;
                    if(entry.ConversionRatioId.ToString() == "ffffffff-ffff-ffff-ffff-ffffffffffff")
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

        private static SelectList FormatConversionRatioForSelectList(ConversionRatioDTO[] conversionRatios)
        {
            var result = conversionRatios.Select(
                g => new {g.Id, Value = g.CurrencyFrom.Code + " - " + g.CurrencyTo.Code + " - " + g.Ratio});
            return  new SelectList(result, "Id", "Value").Add("Custom", "ffffffff-ffff-ffff-ffff-ffffffffffff",SelectListHelper.ListPosition.Last);
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
