using System;
using System.Linq;
using System.Threading.Tasks;
using MyWallet.Entities.Models;

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
        Task<Entry[]> GetEntriesByUser(Guid userId); //TODO: this should be implemented on service using GetAllEntries

        /// <summary>
        /// Returns all entries in selected budget
        /// </summary>
        /// <param name="budgetId">Budget </param>
        /// <returns>All entries by budget</returns>
        Task<Entry[]> GetEntriesByBudget(Guid budgetId); //TODO: this should be implemented on service using 
    }
}