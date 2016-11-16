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
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public BudgetService(
            IBudgetRepository budgetRepository,
            ICategoryRepository categoryRepository,
            ICurrencyRepository currencyRepository,
            IGroupRepository groupRepository,
            ILogger<IBudgetService> logger,
            IMapper mapper)
        {
            _budgetRepository = budgetRepository;
            _categoryRepository = categoryRepository;
            _currencyRepository = currencyRepository;
            _groupRepository = groupRepository;
            _logger = logger;
            _mapper = mapper;
        }


        public async Task<BudgetDTO> AddBudget(BudgetDTO budget, Guid currencyId, Guid groupId, ICollection<Guid> categoryIds)
        {
            
            var dataAccessBudgetModel = _mapper.Map<Budget>(budget);
            dataAccessBudgetModel.Group = await _groupRepository.GetSingleGroup(groupId);
            dataAccessBudgetModel.Currency =  await _currencyRepository.GetSingleCurrency(currencyId);
            dataAccessBudgetModel.Categories = await _categoryRepository.GetCategoriesFromIds(categoryIds);
            dataAccessBudgetModel = await _budgetRepository.AddBudget(dataAccessBudgetModel);

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
