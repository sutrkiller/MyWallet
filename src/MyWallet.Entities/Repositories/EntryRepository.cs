﻿using System;
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
    internal class EntryRepository : IEntryRepository
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
            if (entry.Categories == null)
            {
                throw new ArgumentNullException(nameof(Entry.Categories));
            }
            if (entry.ConversionRatio == null)
            {
                throw new ArgumentNullException(nameof(Entry.ConversionRatio));
            }
            if (entry.User == null)
            {
                throw new ArgumentNullException(nameof(Entry.User));
            }
            entry.User = _context.Users.Find(entry.User.Id);
            entry.ConversionRatio = _context.ConversionRatios.Find(entry.ConversionRatio.Id);
            var categories = entry.Categories;
            entry.Categories = new List<Category>();
            foreach (var cat in categories)
            {
                entry.Categories.Add(_context.Categories.Find(cat.Id));
            }
            var budgets = entry.Budgets;
            entry.Budgets = new List<Budget>();
            foreach (var bud in budgets ?? new List<Budget>())
            {
                entry.Budgets.Add(_context.Budgets.Find(bud.Id));
            }

            var addedEntry = _context.Entries.Add(entry);
            await _context.SaveChangesAsync();

            return addedEntry;
        }

        public async Task EditEntry(Entry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }
            if (entry.Categories == null)
            {
                throw new ArgumentNullException(nameof(Entry.Categories));
            }
            if (entry.User == null)
            {
                throw new ArgumentNullException(nameof(Entry.User));
            }
            if (entry.Budgets == null)
            {
                throw new ArgumentNullException(nameof(Entry.Budgets));
            }
            if (entry.ConversionRatio == null)
            {
                throw new ArgumentNullException(nameof(Entry.ConversionRatio));
            }

            var local = await _context.Entries.FindAsync(entry.Id);
            _context.Entry(local).CurrentValues.SetValues(entry);
            local.User = _context.Users.Find(entry.User.Id);
            local.ConversionRatio = _context.ConversionRatios.Find(entry.ConversionRatio.Id);
            foreach (var category in local.Categories)
            {
                category.Entries = category.Entries.Where(x => x.Id != local.Id).ToList();
            }
            var categories = entry.Categories.Select(x => _context.Categories.Find(x.Id)).ToList();
            local.Categories = new HashSet<Category>(categories);
            foreach (var budget in local.Budgets)
            {
                budget.Entries = budget.Entries.Where(x => x.Id != local.Id).ToList();
            }
            var budgets = entry.Budgets.Select(x => _context.Budgets.Find(x.Id)).ToList();
            local.Budgets = new HashSet<Budget>(budgets);
            
            await _context.SaveChangesAsync();
        }

        public async Task<Entry> GetSingleEntry(Guid id)
          => await _context
                .Entries
                .Where(entry => entry.Id == id)
                .SingleOrDefaultAsync();

        public IQueryable<Entry> GetAllEntries()
         => _context.Entries.AsQueryable();

        public IQueryable<Entry> GetEntriesByUser(Guid userId)
          => _context
                .Entries
                .Where(entry => entry.User.Id == userId);

        public IQueryable<Entry> GetEntriesByBudget(Guid budgetId)
            => _context
                .Entries
                .Where(entry => entry.Budgets.Any(budget => budget.Id == budgetId));

        public async Task DeleteEntry(Entry entry)
        {
            _context.Entries.Remove(entry);
            await _context.SaveChangesAsync();
        }
    }
}