using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MyWallet.Entities.Models;
using MyWallet.Entities.Repositories.Interfaces;
using MyWallet.Services.DataTransferModels;
using MyWallet.Services.Services.Interfaces;

namespace MyWallet.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ILogger<IBudgetService> _logger;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(
            ICategoryRepository categoryRepository,
            ILogger<IBudgetService> logger,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> AddCategory(CategoryDTO category)
        {
            var dataAccessCategoryModel = _mapper.Map<Category>(category);
            dataAccessCategoryModel = await _categoryRepository.AddCategory(dataAccessCategoryModel);
            return _mapper.Map<CategoryDTO>(dataAccessCategoryModel);
        }

        public void EditCategory(CategoryDTO category)
        {
            var dataAccessCategoryModel = _mapper.Map<Category>(category);
            _categoryRepository.EditCategory(dataAccessCategoryModel);
        }

        public async Task<CategoryDTO[]> GetAllCategories()
        {
            _logger.LogInformation("Starting Category service method");

            var categories = await _categoryRepository.GetAllCategories().ToArrayAsync();
            return _mapper.Map<CategoryDTO[]>(categories);
        }

        public async Task<CategoryDTO> GetCategory(Guid id)
        {
            var category = await _categoryRepository.GetSingleCategory(id);

            return _mapper.Map<CategoryDTO>(category);
        }
    }
}
