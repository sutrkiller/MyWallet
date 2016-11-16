using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWallet.Entities.Models;
using MyWallet.Models.Budgets;
using MyWallet.Services.DataTransferModels;
using MyWallet.Services.Services.Interfaces;

namespace MyWallet.Controllers
{
    public class BudgetsController : Controller
    {
        private readonly IBudgetService _budgetService;
        private readonly IEntryService _entryService;
        private readonly IMapper _mapper;

        public BudgetsController(IBudgetService budgetService, IEntryService entryService, IMapper mapper)
        {
            _budgetService = budgetService;
            _entryService = entryService;
            _mapper = mapper;
        }

        // GET: Budgets
        public async Task<IActionResult> List()
        {
            var budgetsDTO = await _budgetService.GetAllBudgets();
            return View(_mapper.Map<IEnumerable<BudgetViewModel>>(budgetsDTO));
        }

        // GET: Budgets/Details/[id]
        public async Task<IActionResult> Details(Guid id)
        {

            var budgetDTO = await _budgetService.GetBudget(id);
            if (budgetDTO == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<BudgetDetailsViewModel>(budgetDTO);
            return View(model);
        }

        // GET: Budgets/Create
        public async Task<IActionResult> Create()
        {
            var newBudget = new CreateBudgetViewModel();
            var categories = await _budgetService.GetAllCategories();
            var categoriesList = categories.Select(d => new { Id = d.Id, Value = d.Name });
            newBudget.CategoriesList = new SelectList(categoriesList, "Id", "Value");
            var currencies = await _entryService.GetAllCurrencies();
            var currenciesList = currencies.Select(g => new { g.Id, Value = g.Code });
            newBudget.CurrenciesList = new SelectList(currenciesList, "Id", "Value");
            var groups = await _budgetService.GetAllGroups();
            var groupsList = groups.Select(g => new {g.Id, Value = g.Name});
            newBudget.GroupsList = new SelectList(groupsList,"Id","Value");
            return View(newBudget);
        }

        // POST: Budgets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBudgetViewModel budget)
        {
            if (ModelState.IsValid)
            {
                await _budgetService.AddBudget(_mapper.Map<BudgetDTO>(budget), budget.CurrencyId, budget.GroupId, budget.CategoryIds);
                return RedirectToAction("List");
            }
            return View(budget);
        }
    }
}
