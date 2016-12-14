using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyWallet.Services.DataTransferModels;
using MyWallet.Services.Filters;

namespace MyWallet.Services.Services.Interfaces
{
    /// <summary>
    /// Entry service. Layer above entry repository
    /// </summary>
    public interface IEntryService
    {
        /// <summary>
        /// Add new entry
        /// </summary>
        /// <param name="entry">Entry with filled values</param>
        /// <param name="userEmail">Email of entry owner</param>
        /// <param name="conversionRatioId">Id of requested conversion ratio</param>
        /// <param name="categoryId">Id of existing cateogry</param>
        /// <param name="budgetIds">Ids of existing budgets</param>
        /// <returns>New entry with id</returns>
        Task<Entry> AddEntry(Entry entry, string userEmail, Guid conversionRatioId, ICollection<Guid> categoryId, ICollection<Guid> budgetIds);
        /// <summary>
        /// Edit existing entry
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="userEmail"></param>
        /// <param name="conversionRatioId"></param>
        /// <param name="categoryId"></param>
        /// <param name="budgetIds"></param>
        /// <returns></returns>
        Task EditEntry(Entry entry, string userEmail, Guid conversionRatioId, ICollection<Guid> categoryId, ICollection<Guid> budgetIds);
        /// <summary>
        /// Return all currencies
        /// </summary>
        /// <returns></returns>
        Task<Currency[]> GetAllCurrencies();
        /// <summary>
        /// Return single entry by id
        /// </summary>
        /// <param name="entryId">Id of existing entry</param>
        /// <returns></returns>
        Task<Entry> GetEntry(Guid entryId);
        /// <summary>
        /// Returns all entries filtered by filter
        /// </summary>
        /// <param name="filter">Custom filter</param>
        /// <returns>Filtered entries</returns>
        Task<Entry[]> GetAllEntries(EntriesFilter filter = null);
        /// <summary>
        /// Get all conversion ratios in db
        /// </summary>
        /// <returns></returns>
        Task<ConversionRatio[]> GetAllConversionRatios();
        /// <summary>
        /// Get all conversion ratios that are from Currency with Id = currencyId
        /// </summary>
        /// <param name="currencyId"></param>
        /// <returns></returns>
        Task<ConversionRatio[]> GetConversionRatiosForCurrency(Guid currencyId);
        /// <summary>
        /// Creates new Conversion Ratios
        /// </summary>
        /// <param name="ratios">Ratios with filled values</param>
        /// <returns></returns>
        Task AddConversionRatios(IEnumerable<ConversionRatio> ratios);
        /// <summary>
        /// Delete entry by id
        /// </summary>
        /// <param name="id">Id of existing entry</param>
        /// <returns></returns>
        Task DeleteEntry(Guid id);
        /// <summary>
        /// Add single conversion ratio
        /// </summary>
        /// <param name="currencyId">Id of existing currency</param>
        /// <param name="customRatioAmount">Converting ratio</param>
        /// <param name="customRatioCurrencyId">To currency</param>
        /// <returns></returns>
        Task<ConversionRatio> AddConversionRatio(Guid currencyId, string customRatioAmount, Guid customRatioCurrencyId);
    }
}
