using System;
using System.Threading.Tasks;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Services.Services.Interfaces
{
    /// <summary>
    /// Category service. Layer above category repository
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Add category to db
        /// </summary>
        /// <param name="category">Category with filled values</param>
        /// <returns></returns>
        Task<Category> AddCategory(Category category);
        /// <summary>
        /// Returns all categories
        /// </summary>
        /// <returns>All categories in db</returns>
        Task<Category[]> GetAllCategories();
        /// <summary>
        /// Retrieve single category by id
        /// </summary>
        /// <param name="id">Id of existing category</param>
        /// <returns></returns>
        Task<Category> GetCategory(Guid id);
        /// <summary>
        /// Edit category with new values
        /// </summary>
        /// <param name="category">Category with valid Id</param>
        /// <returns></returns>
        Task EditCategory(Category category);
        /// <summary>
        /// Deletes category with id
        /// </summary>
        /// <param name="id">Id of existing category</param>
        /// <returns></returns>
        Task DeleteCategory(Guid id);
    }
}
