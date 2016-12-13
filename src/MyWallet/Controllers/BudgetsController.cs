using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWallet.Helpers;
using MyWallet.Models.Budgets;
using MyWallet.Services.DataTransferModels;
using MyWallet.Services.Filters;
using MyWallet.Services.Services.Interfaces;
using Sakura.AspNetCore;

namespace MyWallet.Controllers
{
    public class BudgetsController : Controller
    {
        private readonly IBudgetService _budgetService;
        private readonly IEntryService _entryService;
        private readonly IUserService _userService;
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        public BudgetsController(IBudgetService budgetService, IMapper mapper, IEntryService entryService, IUserService userService, IGroupService groupService)
        {
            _budgetService = budgetService;
            _mapper = mapper;
            _entryService = entryService;
            _userService = userService;
            _groupService = groupService;
        }

        private const int PageSize = 10;
        // GET: Budgets
        [Authorize]
        public async Task<IActionResult> List(int? page = null)
        {
            var userId = await _userService.GetUserId(User.Identity as ClaimsIdentity);
            var budgets = new BudgetDTO[0];
            if (userId != null)
            {
                budgets = await _budgetService.GetAllBudgets(new BudgetFilter {UserId = userId});
            }
            
            int pageNumber = page ?? 1;
            return View("List", _mapper.Map<IEnumerable<BudgetViewModel>>(budgets.OrderBy(x => x.Name)).ToPagedList(PageSize, pageNumber));

            
        }

        // GET: Budgets/Details/[id]
        [Authorize]
        public async Task<IActionResult> Details(Guid id)
        {

            var budget = await _budgetService.GetBudget(id);
            if (budget == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<BudgetDetailsViewModel>(budget);

            var entries = budget.Entries;
            model.Entries = string.Join("\n", entries.Select(x => x.Description + " - " + x.Amount.FormatCurrency(x.ConversionRatio.CurrencyFrom.Code)));
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
            newBudget.StartDate = DateTime.Today.ToString("MM/dd/yyyy");
            newBudget.EndDate = DateTime.Today.ToString("MM/dd/yyyy");
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

            var groups = new GroupDTO[0];
            var userId = await _userService.GetUserId(User.Identity as ClaimsIdentity);
            if (userId != null)
            {
                groups = await _groupService.GetAllGroups(new GroupFilter {UserId = userId});
                var user = await _userService.GetUser(userId.Value);
                newBudget.CurrencyId = user.PreferredCurrency.Id;
            }
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

            var groups = new GroupDTO[0];
            var userId = await _userService.GetUserId(User.Identity as ClaimsIdentity);
            if (userId != null)
            {
                groups = await _groupService.GetAllGroups(new GroupFilter { UserId = userId });
            }
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
            DateTime start;
            DateTime end;
            bool correct = true;
            if (!DateTime.TryParseExact(budget.StartDate, "MM/dd/yyyy", new CultureInfo("en-US"), DateTimeStyles.None,
                out start))
            {
                correct = false;
                ModelState.AddModelError("StartDate","Wrong format of date.");
            }
            if (!DateTime.TryParseExact(budget.EndDate, "MM/dd/yyyy", new CultureInfo("en-US"), DateTimeStyles.None,
               out end))
            {
                correct = false;
                ModelState.AddModelError("EndDate", "Wrong format of date.");
            }
            if (!correct || end < start)
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
            DateTime start;
            DateTime end;
            bool correct = true;
            if (!DateTime.TryParseExact(budget.StartDate, "MM/dd/yyyy", new CultureInfo("en-US"), DateTimeStyles.None,
                out start))
            {
                correct = false;
                ModelState.AddModelError("StartDate", "Wrong format of date.");
            }
            if (!DateTime.TryParseExact(budget.EndDate, "MM/dd/yyyy", new CultureInfo("en-US"), DateTimeStyles.None,
               out end))
            {
                correct = false;
                ModelState.AddModelError("EndDate", "Wrong format of date.");
            }
            if (!correct || end < start)
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
