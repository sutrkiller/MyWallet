using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyWallet.Entities.DataAccessModels;

namespace MyWallet.Entities.Repositories.Interfaces
{
    public interface IBudgetRepository
    {
        /// <summary>
        /// Adds single budget
        /// </summary>
        /// <param name="budget">New budget</param>
        /// <param name="currency">Used currency</param>
        /// <param name="categories">Used categories</param>
        /// <returns>Added budget</returns>
        Task<Budget> AddBudget(Budget budget, Currency currency, ICollection<Category> categories);

        /// <summary>
        /// Returns single budget
        /// </summary>
        /// <param name="id">Guid of budget</param>
        /// <returns>Budget by id</returns>
        Task<Budget> GetSingleBudget(Guid id);

        /// <summary>
        /// Returns all budgets
        /// </summary>
        /// <returns>All budgets</returns>
        Task<Budget[]> GetAllBudgets();
    }
}