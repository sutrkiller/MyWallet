using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyWallet.Services.Filters;
using Budget = MyWallet.Services.DataTransferModels.Budget;
using Category = MyWallet.Services.DataTransferModels.Category;

namespace MyWallet.Services.Services.Interfaces
{
    /// <summary>
    /// Service for budget. Layer above repository
    /// </summary>
    public interface IBudgetService
    {
        /// <summary>
        /// Creates budget
        /// </summary>
        /// <param name="budget">Budget entity with set parameters</param>
        /// <param name="groupId">Id of existing group</param>
        /// <param name="currencyId">Id of existing currency</param>
        /// <param name="categoryIds">Ids of existing categories</param>
        /// <returns>Created budget</returns>
        Task<Budget> AddBudget(Budget budget, Guid groupId, Guid currencyId, ICollection<Guid> categoryIds);
        /// <summary>
        /// Edit budget
        /// </summary>
        /// <param name="budget">Budget with valid id and edited values.</param>
        /// <param name="groupId">If of existing group</param>
        /// <param name="currencyId">Id of existing currency</param>
        /// <param name="categoryIds">Id of existing categories</param>
        /// <returns></returns>
        Task EditBudget(Budget budget, Guid groupId, Guid currencyId, ICollection<Guid> categoryIds);
        /// <summary>
        /// Retrieve all budgets
        /// </summary>
        /// <param name="filter">Filter to select only some budgets</param>
        /// <returns>Budgets that were filtered</returns>
        Task<Budget[]> GetAllBudgets(BudgetFilter filter = null);
        /// <summary>
        /// Get budget by id
        /// </summary>
        /// <param name="id">Id of existing budget</param>
        /// <returns></returns>
        Task<Budget> GetBudget(Guid id);
        /// <summary>
        /// Get all categories in db
        /// </summary>
        /// <returns>All categories from db</returns>
        Task<Category[]> GetAllCategories();
        
        /// <summary>
        /// Delete existing budget by id
        /// </summary>
        /// <param name="id">Id of existing budget</param>
        /// <returns></returns>
        Task DeleteBudget(Guid id);
        /// <summary>
        /// Budget that contains latest entry by user
        /// </summary>
        /// <param name="userId">Owner of the latest entry</param>
        /// <returns>Latest used budget</returns>
        Task<Budget> GetLastUsedBudget(Guid userId);
    }
}
