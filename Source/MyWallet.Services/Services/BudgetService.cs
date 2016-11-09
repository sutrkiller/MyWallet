using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MyWallet.Services.DataTransferModels;
using MyWallet.Services.Services.Interfaces;
using MyWallet.Entities.Repositories.Interfaces;
using MyWallet.Entities.Repositories.Interfaces.MyWallet.Entities.Repositories.Interfaces;

namespace MyWallet.Services.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly ILogger<IBudgetService> _logger;
        private readonly IBudgetRepository _budgetRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMapper _mapper;

        public BudgetService(
            IBudgetRepository budgetRepository,
            ICategoryRepository categoryRepository,
            ICurrencyRepository currencyRepository,
        ILogger<IBudgetService> logger,
            IMapper mapper)
        {
            _budgetRepository = budgetRepository;
            _categoryRepository = categoryRepository;
            _currencyRepository = currencyRepository;
            _logger = logger;
            _mapper = mapper;
        }


        public async Task<Budget> AddBudget(Budget budget, Guid currencyId, ICollection<Guid> categoryIds)
        {
            var currency = await _currencyRepository.GetSingleCurrency(currencyId);
            var categories = await _categoryRepository.GetCategoriesFromIds(categoryIds);
            var dataAccessBudgetModel = _mapper.Map<Entities.DataAccessModels.Budget>(budget);
            dataAccessBudgetModel = await _budgetRepository.AddBudget(dataAccessBudgetModel, currency, categories);

            return _mapper.Map<Budget>(dataAccessBudgetModel);
        }

        public async Task<Budget[]> GetAllBudgets()
        {
            _logger.LogInformation("Starting Budget service method");

            var dataAccessBudgetsModel = await _budgetRepository.GetAllBudgets();
            return _mapper.Map<Budget[]>(dataAccessBudgetsModel);
        }

        public async Task<Budget> GetBudget(Guid id)
        {
            var budget = await _budgetRepository.GetSingleBudget(id);

            return _mapper.Map<Budget>(budget);
        }

        public async Task<Category[]> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllCategories();
            return _mapper.Map<Category[]>(categories);
        }

        public async Task<Currency[]> GetAllCurrencies()
        {
            var currencies = await _currencyRepository.GetAllCurrencies();
            return _mapper.Map<Currency[]>(currencies);
        }
    }
}
