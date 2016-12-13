using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Services.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> AddCategory(Category category);
        Task<Category[]> GetAllCategories();
        Task<Category> GetCategory(Guid id);
        Task EditCategory(Category category);
        Task DeleteCategory(Guid id);
    }
}
