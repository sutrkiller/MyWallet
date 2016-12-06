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
        Task<EntryDTO> AddEntry(EntryDTO entry, string userEmail, Guid conversionRatioId, ICollection<Guid> categoryId, ICollection<Guid> budgetIds);
        Task EditEntry(EntryDTO entry, string userEmail, Guid conversionRatioId, ICollection<Guid> categoryId, ICollection<Guid> budgetIds);
        Task<EntryDTO[]> GetEntriesByUser(Guid userId);
        Task<CurrencyDTO[]> GetAllCurrencies();
        Task<EntryDTO> GetEntry(Guid entryId);
        Task<EntryDTO[]> GetAllEntries(EntriesFilter filter = null);
        Task<ConversionRatioDTO[]> GetAllConversionRatios();
        Task<EntryDTO[]> GetAllEntriesForBudget(Guid budgetId);
        Task<ConversionRatioDTO[]> GetConversionRatiosForCurrency(Guid currencyId);
        Task AddConversionRatios(IEnumerable<ConversionRatioDTO> ratios);
        Task DeleteEntry(Guid id);
        Task<ConversionRatioDTO> AddConversionRatio(Guid currencyId, string customRatioAmount, Guid customRatioCurrencyId);
    }
}
