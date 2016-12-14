using System;
using System.Linq;
using System.Threading.Tasks;
using MyWallet.Entities.Models;

namespace MyWallet.Entities.Repositories.Interfaces
{
    /// <summary>
    /// Repository for accessing entities Entry in db.
    /// </summary>
    public interface IEntryRepository
    {
        /// <summary>
        /// Adds single entry
        /// </summary>
        /// <param name="entry">New entry</param>
        /// <returns>Added entry</returns>
        Task<Entry> AddEntry(Entry entry);

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
        IQueryable<Entry> GetAllEntries();

        /// <summary>
        /// Returns all entries for selected user
        /// </summary>
        /// <param name="userId">User </param>
        /// <returns>All entries by user</returns>
        IQueryable<Entry> GetEntriesByUser(Guid userId);

        /// <summary>
        /// Returns all entries in selected budget
        /// </summary>
        /// <param name="budgetId">Budget </param>
        /// <returns>All entries by budget</returns>
        IQueryable<Entry> GetEntriesByBudget(Guid budgetId);
        
        /// <summary>
        /// Delete entry from db
        /// </summary>
        /// <param name="entry">Existing entry</param>
        /// <returns></returns>
        Task DeleteEntry(Entry entry);


        /// <summary>
        /// Edit entry in db
        /// </summary>
        /// <param name="entry">Existing entry with valid ID</param>
        /// <returns></returns>
        Task EditEntry(Entry entry);
    }
}