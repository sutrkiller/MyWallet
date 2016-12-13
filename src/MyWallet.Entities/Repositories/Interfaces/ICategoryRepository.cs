using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWallet.Entities.Models;

namespace MyWallet.Entities.Repositories.Interfaces
{
    /// <summary>
    /// Repository for accessing entities Category in db.
    /// </summary>
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
        IQueryable<Category> GetAllCategories();
        /// <summary>
        /// Returns all categories
        /// </summary>
        /// <returns>Return specific categories</returns>
        IQueryable<Category> GetCategoriesFromIds(ICollection<Guid> categoryIds);

        /// <summary>
        /// Edit single Category
        /// </summary>
        /// <param name="category">existing category</param>
        /// <returns>Edit category</returns>
        Task EditCategory(Category category);

        /// <summary>
        /// Delete category from db
        /// </summary>
        /// <param name="category">Existing category that should have valid ID</param>
        /// <returns></returns>
        Task DeleteCategory(Category category);
    }
}
