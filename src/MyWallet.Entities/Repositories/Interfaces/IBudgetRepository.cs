using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWallet.Entities.Models;

namespace MyWallet.Entities.Repositories.Interfaces
{
    /// <summary>
    /// Repository for accessing entities Budget in db.
    /// </summary>
    public interface IBudgetRepository
    {
        /// <summary>
        /// Adds single budget to db.
        /// </summary>
        /// <param name="budget">New budget</param>
        /// <returns>Added budget</returns>
        Task<Budget> AddBudget(Budget budget);

        /// <summary>
        /// Returns single budget
        /// </summary>
        /// <param name="id">Guid of budget</param>
        /// <returns>Budget by id</returns>
        Task<Budget> GetSingleBudget(Guid id);

        /// <summary>
        /// Returns all budgets as queryable. So further condition can be set without materialization.
        /// </summary>
        /// <returns>All budgets</returns>
        IQueryable<Budget> GetAllBudgets();
        
        /// <summary>
        /// Returns Budgets with given ids
        /// </summary>
        /// <param name="budgetIDs">IDs of budgets to retrieve</param>
        /// <returns></returns>
        IQueryable<Budget> GetBudgetsFromIds(ICollection<Guid> budgetIDs);
        
        /// <summary>
        /// Deletes budget from db. It will be removed from existing entries.
        /// </summary>
        /// <param name="budget">Budget to delet. Should be valid budget, that is in database</param>
        /// <returns></returns>
        Task DeleteBudget(Budget budget);

        /// <summary>
        /// Edit budget in db with new valus. 
        /// </summary>
        /// <param name="budget">Budget with edited values. Should contains valid ID.</param>
        /// <returns></returns>
        Task EditBudget(Budget budget);
    }
}
