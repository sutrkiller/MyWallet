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
            //TODO: get real cr
            dataAccessBudgetModel.ConversionRatio =  _conversionRatioRepository.GetAllConversionRatios().FirstOrDefault(cr=>cr.CurrencyFrom.Id == currencyId);
            dataAccessBudgetModel.Categories = await _categoryRepository.GetCategoriesFromIds(categoryIds);

            dataAccessBudgetModel = await _budgetRepository.AddBudget(dataAccessBudgetModel);

            return _mapper.Map<BudgetDTO>(dataAccessBudgetModel);
        }

        public async Task<BudgetDTO[]> GetAllBudgets()
        {
            _logger.LogInformation("Starting Budget service method");

            //TODO: create filters
            await Task.Delay(0); //only to keep async, will be useless after change
            var dataAccessBudgetsModel = _budgetRepository.GetAllBudgets().ToArray();
            return _mapper.Map<BudgetDTO[]>(dataAccessBudgetsModel);
        }

        public async Task<BudgetDTO> GetBudget(Guid id)
        {
            var budget = await _budgetRepository.GetSingleBudget(id);

            return _mapper.Map<BudgetDTO>(budget);
        }

        public async Task<CategoryDTO[]> GetAllCategories()
        {
            //TODO: change this later
            await Task.Delay(0);
            var categories = _categoryRepository.GetAllCategories().ToArray();
            return _mapper.Map<CategoryDTO[]>(categories);
        }

        public async Task<Group[]> GetAllGroups()
        {
            //TODO: change this later
            await Task.Delay(0);
            var groups = _groupRepository.GetAllGroups().ToArray();
            return _mapper.Map<Group[]>(groups);
        }
    }
}
