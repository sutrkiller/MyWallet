using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWallet.Entities.Models;
using MyWallet.Services.DataTransferModels;
using MyWallet.Services.Filters;
using Budget = MyWallet.Services.DataTransferModels.Budget;
using Category = MyWallet.Services.DataTransferModels.Category;

namespace MyWallet.Services.Services.Interfaces
{
    public interface IBudgetService
    {
        Task<Budget> AddBudget(Budget budget, Guid groupId, Guid currencyId, ICollection<Guid> categoryIds);
        Task EditBudget(Budget budget, Guid groupId, Guid currencyId, ICollection<Guid> categoryIds);
        Task<Budget[]> GetAllBudgets(BudgetFilter filter = null);
        Task<Budget> GetBudget(Guid id);
        Task<Category[]> GetAllCategories();
        
        Task DeleteBudget(Guid id);
        Task<Budget> GetLastUsedBudget(Guid userId);
    }
}
