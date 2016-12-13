using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWallet.Entities.Models;
using MyWallet.Helpers;
using MyWallet.Models.Budgets;
using MyWallet.Models.Categories;
using MyWallet.Services.DataTransferModels;
using MyWallet.Services.Filters;
using MyWallet.Services.Services.Interfaces;
using Sakura.AspNetCore;
using Category = MyWallet.Services.DataTransferModels.Category;

namespace MyWallet.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IEntryService _entryService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper, IEntryService entryService, IUserService userService)
        {
            _categoryService  = categoryService;
            _mapper = mapper;
            _entryService = entryService;
            _userService = userService;
        }

        private const int PageSize = 10;

        // GET: Categoreis
        [Authorize]
        public async Task<IActionResult> List(int? page = null)
        {
            var categoriesDTO = await _categoryService.GetAllCategories();
            int pageNumber = page ?? 1;
            return View("List", _mapper.Map<IEnumerable<CategoryViewModel>>(categoriesDTO.OrderBy(x => x.Name)).ToPagedList(PageSize, pageNumber));
        }

        // GET: Categoreis/Details/[id]
        [Authorize]
        public async Task<IActionResult> Details(Guid id)
        {
            
            var categoryDTO = await _categoryService.GetCategory(id);
            if (categoryDTO == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<CategoryDetailsViewModel>(categoryDTO);

            var userId = await _userService.GetUserId(User.Identity as ClaimsIdentity);
            if (userId == null) return View(model);
            
            var user = await _userService.GetUser(userId.Value);
            if (user == null) return View(model);

            var entries = await _entryService.GetAllEntries(new EntriesFilter {UserId = userId,CategoryId = id});
            var crs = await _entryService.GetConversionRatiosForCurrency(user.PreferredCurrency.Id);
            var ratio = crs.OrderByDescending(c => c.Date).FirstOrDefault()?.Ratio ?? 0m;
            var exp = entries.Where(x => x.Amount < 0)
                .Select(x => x.ToCurrency(ratio))
                .Sum();
            var inc = entries.Where(x => x.Amount > 0)
                .Select(x => x.ToCurrency(ratio))
                .Sum();
            model.Expense = exp.FormatCurrency(user.PreferredCurrency.Code);
            model.Income = inc.FormatCurrency(user.PreferredCurrency.Code);
            model.Balance = (inc + exp).FormatCurrency(user.PreferredCurrency.Code);
            return View(model);
            
        }

        [Authorize]
        public async Task<IActionResult> Edit(Guid id)
        {

            var categoryDTO = await _categoryService.GetCategory(id);
            if (categoryDTO == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<EditCategoryViewModel>(categoryDTO);
            return View(model);

        }

        // GET: Categoreis/Create
        [Authorize]
        public IActionResult Create()
        {
            //throw new NotImplementedException();
            
            var newCategory = new CreateCategoryViewModel();
            return View(newCategory);
            
        }

        
        // POST: Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.AddCategory(_mapper.Map<Category>(category));
                return RedirectToAction("List");
            }
            return View(category);
        }

        // POST: Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.EditCategory(_mapper.Map<Category>(category));
                return RedirectToAction("List");
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var category = await _categoryService.GetCategory(id);
            if (category.Entries.Any())
            {
                TempData["ErrorMessage"] = "Category still contains some entries.";
                return RedirectToAction("List");
            }
            await _categoryService.DeleteCategory(id);
            return RedirectToAction("List");
        }
    }
    }
