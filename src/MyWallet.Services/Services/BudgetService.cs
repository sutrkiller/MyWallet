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
using MyWallet.Services.Filters;
using MyWallet.Services.Services.Interfaces;

namespace MyWallet.Services.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly ILogger<IBudgetService> _logger;
        private readonly IBudgetRepository _budgetRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IConversionRatioRepository _conversionRatioRepository;
        private readonly IMapper _mapper;

        public BudgetService(
            IBudgetRepository budgetRepository,
            ICategoryRepository categoryRepository,
            IGroupRepository groupRepository,
            ILogger<IBudgetService> logger,
            IMapper mapper, ICurrencyRepository currencyRepository, IConversionRatioRepository conversionRatioRepository)
        {
            _budgetRepository = budgetRepository;
            _categoryRepository = categoryRepository;
            _groupRepository = groupRepository;
            _logger = logger;
            _mapper = mapper;
            _currencyRepository = currencyRepository;
            _conversionRatioRepository = conversionRatioRepository;
        }


        public async Task<BudgetDTO> AddBudget(BudgetDTO budget, Guid groupId, Guid currencyId, ICollection<Guid> categoryIds)
        {
            var dataAccessBudgetModel = _mapper.Map<Budget>(budget);
            dataAccessBudgetModel.Group = await _groupRepository.GetSingleGroup(groupId);
            dataAccessBudgetModel.ConversionRatio =  _conversionRatioRepository.GetAllConversionRatios().OrderByDescending(x=>x.Date).FirstOrDefault(cr=>cr.CurrencyFrom.Id == currencyId);
            dataAccessBudgetModel.Categories = await _categoryRepository.GetCategoriesFromIds(categoryIds).ToArrayAsync();

            dataAccessBudgetModel = await _budgetRepository.AddBudget(dataAccessBudgetModel);

            return _mapper.Map<BudgetDTO>(dataAccessBudgetModel);
        }

        public async Task EditBudget(BudgetDTO budget, Guid groupId, Guid currencyId, ICollection<Guid> categoryIds)
        {
            var dataAccessBudgetModel = _mapper.Map<Budget>(budget);
            dataAccessBudgetModel.Group = await _groupRepository.GetSingleGroup(groupId);
            dataAccessBudgetModel.ConversionRatio = _conversionRatioRepository.GetAllConversionRatios().OrderByDescending(x => x.Date).FirstOrDefault(cr => cr.CurrencyFrom.Id == currencyId);
            dataAccessBudgetModel.Categories = await _categoryRepository.GetCategoriesFromIds(categoryIds).ToArrayAsync();

            await _budgetRepository.EditBudget(dataAccessBudgetModel);
            
        }

        public async Task<BudgetDTO[]> GetAllBudgets(BudgetFilter filter = null)
        {
            _logger.LogInformation("Starting Budget service method");
            var budgets = _budgetRepository.GetAllBudgets();

            if (filter != null)
            {
                if (filter.UserId.HasValue)
                {
                    budgets = budgets.Where(x => x.Group.Users.Any(u => u.Id == filter.UserId.Value));
                }
            }

            var dataAccessBudgetsModel = await budgets.OrderBy(x=>x.Name).ToArrayAsync();
            return _mapper.Map<BudgetDTO[]>(dataAccessBudgetsModel);
        }

        public async Task<BudgetDTO> GetBudget(Guid id)
        {
            var budget = await _budgetRepository.GetSingleBudget(id);

            return _mapper.Map<BudgetDTO>(budget);
        }

        public async Task<CategoryDTO[]> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllCategories().OrderBy(x=>x.Name).ToArrayAsync();
            return _mapper.Map<CategoryDTO[]>(categories);
        }

        

        public async Task DeleteBudget(Guid id)
        {
            var budget = await _budgetRepository.GetSingleBudget(id);
            await _budgetRepository.DeleteBudget(budget);
        }

        public async Task<BudgetDTO> GetLastUsedBudget(Guid userId)
        {
            var budget = await 
                _budgetRepository.GetAllBudgets()
                    .Where(x=>x.Group.Users.Any(u=>u.Id == userId))
                    .OrderByDescending(x => x.Entries.Max(e => e.EntryTime))
                    .FirstOrDefaultAsync();
            return _mapper.Map<BudgetDTO>(budget);
        }
    }
}
