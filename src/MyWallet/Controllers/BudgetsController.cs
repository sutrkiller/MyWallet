using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWallet.Models.Budgets;
using MyWallet.Services.DataTransferModels;
using MyWallet.Services.Services.Interfaces;
using Sakura.AspNetCore;

namespace MyWallet.Controllers
{
    public class BudgetsController : Controller
    {
        private readonly IBudgetService _budgetService;
        private readonly IEntryService _entryService;
        private readonly IMapper _mapper;

        public BudgetsController(IBudgetService budgetService, IMapper mapper, IEntryService entryService)
        {
            _budgetService = budgetService;
            _mapper = mapper;
            _entryService = entryService;
        }

        private const int PageSize = 10;
        // GET: Budgets
        [Authorize]
        public async Task<IActionResult> List(int? page = null)
        {
            var budgetsDTO = await _budgetService.GetAllBudgets();
            int pageNumber = page ?? 1;
            return View("List", _mapper.Map<IEnumerable<BudgetViewModel>>(budgetsDTO.OrderBy(x => x.Name)).ToPagedList(PageSize, pageNumber));

            
        }

        // GET: Budgets/Details/[id]
        [Authorize]
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

        [Authorize]
        public async Task<IActionResult> Edit(Guid id)
        {
            var budgetDTO = await _budgetService.GetBudget(id);
            if (budgetDTO == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<EditBudgetViewModel>(budgetDTO);

            await FillSelectionLists(model);

            return View(model);

        }

        // GET: Budgets/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var newBudget = new CreateBudgetViewModel();
            await FillSelectionLists(newBudget);
            newBudget.StartDate = DateTime.Now;
            newBudget.EndDate = DateTime.Now;
            return View(newBudget);



        }

        private async Task FillSelectionLists(CreateBudgetViewModel newBudget)
        {
            var categories = await _budgetService.GetAllCategories();
            var categoriesList = categories.Select(d => new {Id = d.Id, Value = d.Name});
            newBudget.CategoriesList = new SelectList(categoriesList, "Id", "Value");
            var currencies = await _entryService.GetAllCurrencies();
            var currenciesList = currencies.Select(g => new {g.Id, Value = g.Code});
            newBudget.CurrenciesList = new SelectList(currenciesList, "Id", "Value");
            var groups = await _budgetService.GetAllGroups();
            var groupsList = groups.Select(g => new {g.Id, Value = g.Name});
            newBudget.GroupsList = new SelectList(groupsList, "Id", "Value");
        }
        private async Task FillSelectionLists(EditBudgetViewModel newBudget)
        {
            var categories = await _budgetService.GetAllCategories();
            var categoriesList = categories.Select(d => new { Id = d.Id, Value = d.Name });
            newBudget.CategoriesList = new SelectList(categoriesList, "Id", "Value");
            var currencies = await _entryService.GetAllCurrencies();
            var currenciesList = currencies.Select(g => new { g.Id, Value = g.Code });
            newBudget.CurrenciesList = new SelectList(currenciesList, "Id", "Value");
            var groups = await _budgetService.GetAllGroups();
            var groupsList = groups.Select(g => new { g.Id, Value = g.Name });
            newBudget.GroupsList = new SelectList(groupsList, "Id", "Value");
        }

        // POST: Budgets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBudgetViewModel budget)
        {
            if (budget.EndDate < budget.StartDate)
            {
                ModelState.AddModelError("EndDate", "End Date have to be after Start Date.");
            }
            if (ModelState.IsValid)
            {
                await _budgetService.AddBudget(_mapper.Map<BudgetDTO>(budget), budget.GroupId, budget.CurrencyId, budget.CategoryIds);
                return RedirectToAction("List");
            }
            await FillSelectionLists(budget);
            return View(budget);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditBudgetViewModel budget)
        {
            if (budget.EndDate<budget.StartDate)
            {
                ModelState.AddModelError("EndDate", "End Date have to be after Start Date.");
            }
            if (ModelState.IsValid)
            {
                await _budgetService.EditBudget(_mapper.Map<BudgetDTO>(budget), budget.GroupId, budget.CurrencyId, budget.CategoryIds);
                return RedirectToAction("List");
            }
            await FillSelectionLists(budget);
            return View(budget);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _budgetService.DeleteBudget(id);
            return RedirectToAction("List");
        }
    }
}
