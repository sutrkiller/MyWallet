﻿using System;
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