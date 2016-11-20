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

        // GET: Categoreis
        [Authorize]
        public async Task<IActionResult> List()
        {
            var categoriesDTO = await _categoryService.GetAllCategories();
            return View(_mapper.Map<IEnumerable<CategoryViewModel>>(categoriesDTO));
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

        // GET: Categoreis/Create
        [Authorize]
        public async Task<IActionResult> Create()
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
        }
    }
