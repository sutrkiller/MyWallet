﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWallet.Entities.Models;

namespace MyWallet.Entities.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        /// <summary>
        /// Adds single Category
        /// </summary>
        /// <param name="category">New category</param>
        /// <returns>Added category</returns>
        Task<Category> AddCategory(Category category);

        /// <summary>
        /// Returns single category
        /// </summary>
        /// <param name="id">Guid of category</param>
        /// <returns>Category by id</returns>
        Task<Category> GetSingleCategory(Guid id);

        /// <summary>
        /// Returns all categories
        /// </summary>
        /// <returns>All categories</returns>
        Task<Category[]> GetAllCategories();
        /// <summary>
        /// Returns all categories
        /// </summary>
        /// <returns>Return specific categories</returns>
        Task<Category[]> GetCategoriesFromIds(ICollection<Guid> categoryIds);
    }
}