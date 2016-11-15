using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MyWallet.Entities.Models;
using MyWallet.Entities.Repositories.Interfaces;
using MyWallet.Services.DataTransferModels;
using MyWallet.Services.Services.Interfaces;

namespace MyWallet.Services.Services
{
    public class EntryService : IEntryService
    {
        private readonly ILogger<IBudgetService> _logger;
        private readonly IBudgetRepository _budgetRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEntryRepository _entryRepository;
        private readonly IMapper _mapper;

        public async Task<EntryDTO> AddEntry(EntryDTO entry)
        {
            //todo: download conversion from net
            var entryDb = _mapper.Map<Entry>(entry);
            entryDb = await _entryRepository.AddEntry(entryDb);

            return _mapper.Map<EntryDTO>(entryDb);
        }

        public Task<EntryDTO[]> GetEntriesByUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<CurrencyDTO[]> GetAllCurrencies()
        {
            throw new NotImplementedException();
        }

        public Task<EntryDTO> GetEntry(Guid entryId)
        {
            throw new NotImplementedException();
        }

        public Task<EntryDTO> GetAllEntries(Guid entryId)
        {
            throw new NotImplementedException();
        }

        public Task<EntryDTO> GetAllEntriesForBudget(Guid budgetId)
        {
            throw new NotImplementedException();
        }
    }
}
