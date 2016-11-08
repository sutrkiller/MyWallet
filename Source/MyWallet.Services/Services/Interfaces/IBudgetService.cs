using System;
using System.Threading.Tasks;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Services.Services.Interfaces
{
    public interface IBudgetService
    {
        Task<Budget> AddBudget(Budget budget);
        Task<Budget[]> GetAllBudgets();
        Task<Budget> GetBudget(Guid id);
    }
}