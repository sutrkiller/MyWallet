using System;
using System.Collections.Generic;
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
        private readonly IMapper _mapper;

        public EntriesController(IEntryService entryService, IMapper mapper, IBudgetService budgetService)
        {
            _entryService = entryService;
            _mapper = mapper;
            _budgetService = budgetService;
        }

        // GET: Budgets
        [Authorize]
        public async Task<IActionResult> List()
        {
            var entriesDTO = await _entryService.GetAllEntries();
            return View(_mapper.Map<IEnumerable<EntryViewModel>>(entriesDTO));
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
            var categoriesList = categories.Select(d => new { Id = d.Id, Value = d.Name });
            newEntry.CategoriesList = new SelectList(categoriesList, "Id", "Value");
            var budgets = await _budgetService.GetAllBudgets();
            var budgetsList = budgets.Select(g => new { g.Id, Value = g.Name });
            newEntry.BudgetsList = new SelectList(budgetsList, "Id", "Value");
            var currencies = await _entryService.GetAllCurrencies();
            var currenciesList = currencies.Select(g => new { g.Id, Value = g.Code });
            newEntry.CurrenciesList = new SelectList(currenciesList, "Id", "Value");
            var onversionRatios = await _entryService.GetAllConversionRatios();
            var onversionRatiosList = onversionRatios.Select(g => new { g.Id, Value = g.CurrencyFrom.Code + " - " + g.CurrencyTo.Code + " - " + g.Ratio });
            newEntry.ConversionRatiosList = new SelectList(onversionRatiosList, "Id", "Value");
                        return View(newEntry);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEntryViewModel entry)
        {
            if (ModelState.IsValid)
            {
                await _entryService.AddEntry(_mapper.Map<EntryDTO>(entry), entry.UserId, entry.ConversionRatioId, entry.CategoryIds, entry.BudgetIds);
                return RedirectToAction("List");
            }
            return View(entry);
        }
    }
}
