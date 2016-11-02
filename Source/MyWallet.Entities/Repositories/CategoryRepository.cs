﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MyWallet.Entities.Configuration;
using MyWallet.Entities.Contexts;
using MyWallet.Entities.DataAccessModels;
using MyWallet.Entities.Repositories.Interfaces.MyWallet.Entities.Repositories.Interfaces;

namespace MyWallet.Entities.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private MyWalletContext _context;

        public CategoryRepository(IOptions<ConnectionOptions> connectionOptions)
        {
            _context = new MyWalletContext(connectionOptions.Value.ConnectionString);
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
    }
}