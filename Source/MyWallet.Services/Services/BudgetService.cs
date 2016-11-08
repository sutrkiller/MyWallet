using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MyWallet.Services.DataTransferModels;
using MyWallet.Services.Services.Interfaces;
using MyWallet.Entities.Repositories.Interfaces;

namespace MyWallet.Services.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly ILogger<IBudgetService> _logger;
        private readonly IBudgetRepository _budgetRepository;
        private readonly IMapper _mapper;

        public BudgetService(
            IBudgetRepository budgetRepository,           
            ILogger<IBudgetService> logger,
            IMapper mapper)
        {
            _budgetRepository = budgetRepository;
            _logger = logger;
            _mapper = mapper;
        }


        public async Task<Budget> AddBudget(Budget budget)
        {
            var dataAccessBudgetModel = _mapper.Map<Entities.DataAccessModels.Budget>(budget);
            dataAccessBudgetModel = await _budgetRepository.AddBudget(dataAccessBudgetModel);

            return _mapper.Map<Budget>(dataAccessBudgetModel);
        }

        public async Task<Budget[]> GetAllBudgets()
        {
            _logger.LogInformation("Starting Budget service method");

            var dataAccessBudgetsModel = await _budgetRepository.GetAllBudgets();
            return _mapper.Map<Budget[]>(dataAccessBudgetsModel);
        }

        public Task<Budget> GetBudget(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
