﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWallet.Entities.Models;

namespace MyWallet.Entities.Repositories.Interfaces
{
    public interface IBudgetRepository
    {
        /// <summary>
        /// Adds single budget to db.
        /// </summary>
        /// <param name="budget">New budget</param>
        /// <param name="group">Group that created budget. Must be already in db.</param>
        /// <param name="categories">Used categories. Muset be already in db.</param>
        /// <returns>Added budget</returns>
        Task<Budget> AddBudget(Budget budget, Group group, ICollection<Category> categories);

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