using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Services.Services.Interfaces
{
    public interface IEntryService
    {
        Task<EntryDTO> AddEntry(EntryDTO entry, Guid userId, Guid conversionRatioId, ICollection<Guid> categoryId, ICollection<Guid> budgetIds );
        Task<EntryDTO[]> GetEntriesByUser(Guid userId);
        Task<CurrencyDTO[]> GetAllCurrencies();
        Task<EntryDTO> GetEntry(Guid entryId);
        Task<EntryDTO[]> GetAllEntries();
        Task<ConversionRatioDTO[]> GetAllConversionRatios();
        Task<EntryDTO[]> GetAllEntriesForBudget(Guid budgetId);
    }
}
