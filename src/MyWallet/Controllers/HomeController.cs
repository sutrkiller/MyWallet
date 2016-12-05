using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWallet.Models.Entries;
using MyWallet.Models.Home;
using MyWallet.Services.DataTransferModels;
using MyWallet.Services.Services.Interfaces;


namespace MyWallet.Controllers
{
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

        public async Task<IActionResult> Index()
        {
            var dashmodel = new DashboardModel();
            
            var newEntry = new CreateEntryViewModel();
            await FillSelectLists(newEntry);
            newEntry.EntryTime = DateTime.Now;
            dashmodel.Entry = newEntry;
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
            var prefCurrency = (await _userService.EnsureUserExists(User.Identity as ClaimsIdentity)).PreferredCurrency.Id;
            newEntry.CurrencyId = prefCurrency;

        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult EasterEgg()
        {
            return View();
        }
        public IActionResult Graphs()
        {
            return View();
        }
        public IActionResult Stats()
        {
            return View();
        }
        public async Task<IActionResult> Create(CreateEntryViewModel entry)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var email = User.FindFirst(ClaimTypes.Email)?.Value;
                    entry.Amount = entry.IsIncome == true ? entry.Amount : -1 * entry.Amount;
                    var conratios =await  _entryService.GetConversionRatiosForCurrency(entry.CurrencyId);
                    var date = conratios.Max(cr => cr.Date);
                    entry.ConversionRatioId = conratios.FirstOrDefault(x => x.Date == date).Id;
                        
                    entry.EntryTime = DateTime.Now;
                    entry.CategoryIds = new List<Guid>();
                    await _entryService.AddEntry(_mapper.Map<EntryDTO>(entry), email, entry.ConversionRatioId, entry.CategoryIds, entry.BudgetIds);
                    return RedirectToAction("Index");
                }
                catch (NullReferenceException ex)
                {
                    Debug.WriteLine(ex.Message);
                }

            }
            await FillSelectLists(entry);

            var dashboard = new DashboardModel();
            dashboard.Entry = entry;
     
            return View("Index",dashboard);
        }
        public IActionResult AddRatio()
        {
            return View();
        }
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
