using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyWallet.Entities.DataAccessModels;

namespace MyWallet.Entities.Repositories.Interfaces
{
    public interface IEntryRepository
    {
        /// <summary>
        /// Adds single entry
        /// </summary>
        /// <param name="entry">New entry</param>
        /// <returns>Added entry</returns>
        Task<Entry> AddEntry(Entry entry);

        /// <summary>
        /// Adds single entry
        /// </summary>
        /// <param name="entry">New entry</param>
        /// <param name="budget">Budget where entry is added</param>
        /// <returns>Added entry</returns>
        Task<Entry> AddEntryToBudget(Entry entry, Budget budget);

        /// <summary>
        /// Returns single entry
        /// </summary>
        /// <param name="id">Guid of entry </param>
        /// <returns>Entry by id</returns>
        Task<Entry> GetSingleEntry(Guid id);

        /// <summary>
        /// Returns all entries
        /// </summary>
        /// <returns>All entries </returns>
        Task<Entry[]> GetAllEntries();
    }
}
