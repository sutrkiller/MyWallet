using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Services.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDTO> AddCategory(CategoryDTO category);
        Task<CategoryDTO[]> GetAllCategories();
        Task<CategoryDTO> GetCategory(Guid id);
        void EditCategory(CategoryDTO category);
    }
}
