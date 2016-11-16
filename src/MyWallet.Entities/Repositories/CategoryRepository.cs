using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MyWallet.Entities.Configuration;
using MyWallet.Entities.Contexts;
using MyWallet.Entities.Models;
using MyWallet.Entities.Repositories.Interfaces;

namespace MyWallet.Entities.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MyWalletContext _context;

        internal CategoryRepository(MyWalletContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _context = context;
        }

        public CategoryRepository(IOptions<ConnectionOptions> connectionOptions)
             : this(new MyWalletContext(connectionOptions.Value.ConnectionString))
        {

        }

        public async Task<Category> AddCategory(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            var addedCategory = _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return addedCategory;
        }

        public async Task<Category> GetSingleCategory(Guid id)
        => await _context
                .Categories
                .Where(category => category.Id == id)
                .SingleOrDefaultAsync();

        public async Task<Category[]> GetAllCategories()
        => await _context.Categories.ToArrayAsync();

        public async Task<Category[]> GetCategoriesFromIds(ICollection<Guid> categoryIds)
        {
            var categories = _context.Categories;
            return await categories.Where(r => categoryIds.Contains(r.Id)).ToArrayAsync();
        }
    }
}