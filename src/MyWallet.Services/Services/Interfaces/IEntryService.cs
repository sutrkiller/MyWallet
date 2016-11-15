using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Services.Services.Interfaces
{
    public interface IEntryService
    {
        Task<EntryDTO> AddEntry(EntryDTO entry);
        Task<EntryDTO[]> GetEntriesByUser(Guid userId);
        Task<CurrencyDTO[]> GetAllCurrencies();
        Task<EntryDTO> GetEntry(Guid entryId);
        Task<EntryDTO> GetAllEntries(Guid entryId);
        Task<EntryDTO> GetAllEntriesForBudget(Guid budgetId);
    }
}
