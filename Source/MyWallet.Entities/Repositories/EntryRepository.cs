using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MyWallet.Entities.Configuration;
using MyWallet.Entities.Contexts;
using MyWallet.Entities.DataAccessModels;
using MyWallet.Entities.Repositories.Interfaces;

namespace MyWallet.Entities.Repositories
{
    public class EntryRepository : IEntryRepository
    {
        private MyWalletContext _context;

        public EntryRepository(IOptions<ConnectionOptions> connectionOptions)
        {
            _context = new MyWalletContext(connectionOptions.Value.ConnectionString);
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

        public Task<Entry> AddEntryToBudget(Entry entry, Budget budget)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }
            if (budget == null)
            {
                throw new ArgumentNullException(nameof(budget));
            }
            entry.Budgets.Add(budget);
            return  AddEntry(entry);
        }

        public async Task<Entry> GetSingleEntry(Guid id)
          => await _context
                .Entries
                .Where(entry => entry.Id == id)
                .SingleOrDefaultAsync();

        public async Task<Entry[]> GetAllEntries()
         => await _context.Entries.ToArrayAsync();
    }
}