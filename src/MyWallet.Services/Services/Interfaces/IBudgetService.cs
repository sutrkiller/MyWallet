using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWallet.Entities.Models;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Services.Services.Interfaces
{
    public interface IBudgetService
    {
        Task<BudgetDTO> AddBudget(BudgetDTO budget, Guid groupId, Guid currencyId, ICollection<Guid> categoryIds);
        Task EditBudget(BudgetDTO budget, Guid groupId, Guid currencyId, ICollection<Guid> categoryIds);
        Task<BudgetDTO[]> GetAllBudgets();
        Task<BudgetDTO> GetBudget(Guid id);
        Task<CategoryDTO[]> GetAllCategories();
        Task<Group[]> GetAllGroups();
        Task DeleteBudget(Guid id);
    }
}
