using System;
using System.Collections.Generic;
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
    public class BudgetService : IBudgetService
    {
        private readonly ILogger<IBudgetService> _logger;
        private readonly IBudgetRepository _budgetRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public BudgetService(
            IBudgetRepository budgetRepository,
            ICategoryRepository categoryRepository,
            IGroupRepository groupRepository,
            ILogger<IBudgetService> logger,
            IMapper mapper)
        {
            _budgetRepository = budgetRepository;
            _categoryRepository = categoryRepository;
            _groupRepository = groupRepository;
            _logger = logger;
            _mapper = mapper;
        }


        public async Task<BudgetDTO> AddBudget(BudgetDTO budget, Guid groupId, ICollection<Guid> categoryIds)
        {
            var group = await _groupRepository.GetSingleGroup(groupId);
            var categories = await _categoryRepository.GetCategoriesFromIds(categoryIds);
            var dataAccessBudgetModel = _mapper.Map<Budget>(budget);
            dataAccessBudgetModel = await _budgetRepository.AddBudget(dataAccessBudgetModel, group, categories);

            return _mapper.Map<BudgetDTO>(dataAccessBudgetModel);
        }

        public async Task<BudgetDTO[]> GetAllBudgets()
        {
            _logger.LogInformation("Starting Budget service method");

            var dataAccessBudgetsModel = await _budgetRepository.GetAllBudgets();
            return _mapper.Map<BudgetDTO[]>(dataAccessBudgetsModel);
        }

        public async Task<BudgetDTO> GetBudget(Guid id)
        {
            var budget = await _budgetRepository.GetSingleBudget(id);

            return _mapper.Map<BudgetDTO>(budget);
        }

        public async Task<CategoryDTO[]> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllCategories();
            return _mapper.Map<CategoryDTO[]>(categories);
        }

        public async Task<Group[]> GetAllGroups()
        {
            var groups = await _groupRepository.GetAllGroups();
            return _mapper.Map<Group[]>(groups);
        }
    }
}
