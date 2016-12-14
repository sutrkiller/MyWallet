using System;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MyWallet.Entities.Repositories.Interfaces;
using MyWallet.Services.Services.Interfaces;
using Category = MyWallet.Services.DataTransferModels.Category;

namespace MyWallet.Services.Services
{
    internal class CategoryService : ICategoryService
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

        public async Task<Category> AddCategory(Category category)
        {
            var dataAccessCategoryModel = _mapper.Map<Entities.Models.Category>(category);
            dataAccessCategoryModel = await _categoryRepository.AddCategory(dataAccessCategoryModel);
            return _mapper.Map<Category>(dataAccessCategoryModel);
        }

        public async Task EditCategory(Category category)
        {
            var dataAccessCategoryModel = _mapper.Map<Entities.Models.Category>(category);
            await _categoryRepository.EditCategory(dataAccessCategoryModel);
        }

        public async Task<Category[]> GetAllCategories()
        {
            _logger.LogInformation("Starting Category service method");

            var categories = await _categoryRepository.GetAllCategories().ToArrayAsync();
            return _mapper.Map<Category[]>(categories);
        }

        public async Task<Category> GetCategory(Guid id)
        {
            var category = await _categoryRepository.GetSingleCategory(id);

            return _mapper.Map<Category>(category);
        }

        public async Task DeleteCategory(Guid id)
        {
            var budget = await _categoryRepository.GetSingleCategory(id);
            await _categoryRepository.DeleteCategory(budget);
        }
    }
}
