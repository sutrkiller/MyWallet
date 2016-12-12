using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWallet.Entities.Models;
using MyWallet.Models.Budgets;
using MyWallet.Models.Categories;
using MyWallet.Services.DataTransferModels;
using MyWallet.Services.Services.Interfaces;
using Sakura.AspNetCore;

namespace MyWallet.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IEntryService _entryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper, IEntryService entryService)
        {
            _categoryService  = categoryService;
            _mapper = mapper;
            _entryService = entryService;
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
                await _categoryService.AddCategory(_mapper.Map<CategoryDTO>(category));
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
                await _categoryService.EditCategory(_mapper.Map<CategoryDTO>(category));
                return RedirectToAction("List");
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _categoryService.DeleteCategory(id);
            return RedirectToAction("List");
        }
    }
    }
