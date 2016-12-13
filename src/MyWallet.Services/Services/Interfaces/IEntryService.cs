using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWallet.Services.DataTransferModels;
using MyWallet.Services.Filters;

namespace MyWallet.Services.Services.Interfaces
{
    public interface IEntryService
    {
        Task<Entry> AddEntry(Entry entry, string userEmail, Guid conversionRatioId, ICollection<Guid> categoryId, ICollection<Guid> budgetIds);
        Task EditEntry(Entry entry, string userEmail, Guid conversionRatioId, ICollection<Guid> categoryId, ICollection<Guid> budgetIds);
        Task<Entry[]> GetEntriesByUser(Guid userId);
        Task<Currency[]> GetAllCurrencies();
        Task<Entry> GetEntry(Guid entryId);
        Task<Entry[]> GetAllEntries(EntriesFilter filter = null);
        Task<ConversionRatio[]> GetAllConversionRatios();
        Task<Entry[]> GetAllEntriesForBudget(Guid budgetId);
        Task<ConversionRatio[]> GetConversionRatiosForCurrency(Guid currencyId);
        Task AddConversionRatios(IEnumerable<ConversionRatio> ratios);
        Task DeleteEntry(Guid id);
        Task<ConversionRatio> AddConversionRatio(Guid currencyId, string customRatioAmount, Guid customRatioCurrencyId);
    }
}
