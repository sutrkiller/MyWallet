using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Services.Services.Interfaces
{
    public interface IBudgetService
    {
        Task<Budget> AddBudget(Budget budget, Guid currencyId, ICollection<Guid> categoryIds);
        Task<Budget[]> GetAllBudgets();
        Task<Budget> GetBudget(Guid id);
        Task<Category[]> GetAllCategories();
        Task<Currency[]> GetAllCurrencies();
    }
}