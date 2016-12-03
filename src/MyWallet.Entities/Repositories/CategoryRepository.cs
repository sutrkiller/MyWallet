using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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

        public async Task EditCategory(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();         
        }

        public async Task<Category> GetSingleCategory(Guid id)
        => await _context
                .Categories
                .Where(category => category.Id == id)
                .SingleOrDefaultAsync();

        public IQueryable<Category> GetAllCategories()
        => _context.Categories.AsQueryable();

        public IQueryable<Category> GetCategoriesFromIds(ICollection<Guid> categoryIds)
        => _context.Categories.Where(r => categoryIds.Contains(r.Id));
        
    }
}