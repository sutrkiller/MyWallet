using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWallet.Models.Budgets;
using MyWallet.Services.DataTransferModels;
using MyWallet.Services.Services;
using MyWallet.Services.Services.Interfaces;

namespace MyWallet.Controllers
{
    public class BudgetsController : Controller
    {
        private readonly IBudgetService _budgetService;
        private readonly IMapper _mapper;

        public BudgetsController(IBudgetService budgetService, IMapper mapper)
        {
            _budgetService = budgetService;
            _mapper = mapper;
        }

        // GET: Budgets
        [ActionName("Index")]
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

            return View(_mapper.Map<BudgetDetailsViewModel>(budgetDTO));
        }

        // GET: Games/Create
        public async Task<IActionResult> Create()
        {
            var newBudget = new CreateBudgetViewModel();
            var categories = await _budgetService.GetAllCategories();
            var categoriesList = categories.Select(d => new { Id = d.Id, Value = d.Name });
            newBudget.CategoriesList = new SelectList(categoriesList, "Id", "Value");
            var currencies = await _budgetService.GetAllCurrencies();
            var currencyList = currencies.Select(d => new { Id = d.Id, Value = d.Code });
            newBudget.CurrencyList = new SelectList(currencyList, "Id", "Value");
            return View(newBudget);



        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBudgetViewModel budget)
        {
            if (ModelState.IsValid)
            {
                await _budgetService.AddBudget(_mapper.Map<Budget>(budget), budget.CurrencyId, budget.CategoryIds);
                return RedirectToAction("Index");
            }
            return View(budget);
        }
    }
}

