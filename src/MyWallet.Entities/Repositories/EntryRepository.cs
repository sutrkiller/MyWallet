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
    public class EntryRepository : IEntryRepository
    {
        private readonly MyWalletContext _context;

        internal EntryRepository(MyWalletContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _context = context;
        }

        public EntryRepository(IOptions<ConnectionOptions> connectionOptions)
             : this(new MyWalletContext(connectionOptions.Value.ConnectionString))
        {

        }

        public async Task<Entry> AddEntry(Entry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }
            if (entry.Budgets == null)
            {
                throw new ArgumentNullException(nameof(entry.Budgets));
            }
            if (entry.Categories == null)
            {
                throw new ArgumentNullException(nameof(entry.Categories));
            }
            if (entry.ConversionRatio == null)
            {
                throw new ArgumentNullException(nameof(entry.ConversionRatio));
            }
            if (entry.User == null)
            {
                throw new ArgumentNullException(nameof(entry.User));
            }
            entry.User = _context.Users.Find(entry.User.Id);
            entry.ConversionRatio = _context.ConversionRatios.Find(entry.ConversionRatio.Id);
            
            var categs = entry.Categories;
            entry.Categories = new List<Category>();
            foreach (var cat in categs)
            {
                entry.Categories.Add(_context.Categories.Find(cat.Id));
            }
            var budgets = entry.Budgets;
            entry.Budgets = new List<Budget>();
            foreach (var bud in budgets)
            {
                entry.Budgets.Add(_context.Budgets.Find(bud.Id));
            }
            var addedEntry = _context.Entries.Add(entry);
            
            await _context.SaveChangesAsync();

            return addedEntry;
        }

        public async Task<Entry> GetSingleEntry(Guid id)
          => await _context
                .Entries
                .Where(entry => entry.Id == id)
                .SingleOrDefaultAsync();

        public async Task<Entry[]> GetAllEntries()
         => await _context.Entries.ToArrayAsync();

        public async Task<Entry[]> GetEntriesByUser(Guid userId)
          => await _context
                .Entries
                .Where(entry => entry.User.Id == userId)
                .ToArrayAsync();

        public async Task<Entry[]> GetEntriesByBudget(Guid budgetId)
          => await _context
                .Entries
                .Where(entry => entry.Budgets.Any(budget => budget.Id == budgetId))
                .ToArrayAsync();

        public async Task<Currency[]> GetAllCurrencies()
         => await _context.Currencies.ToArrayAsync();
    }
}