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
        private readonly IEntryRepository _entryRepository;
        private readonly IMapper _mapper;

        public EntryService(
            IEntryRepository entryRepository,
            IMapper mapper
            )
        {
            _entryRepository = entryRepository;
            _mapper = mapper;
        }

        public async Task<EntryDTO> AddEntry(EntryDTO entry)
        {
            //todo: download conversion from net
            var entryDb = _mapper.Map<Entry>(entry);
            entryDb = await _entryRepository.AddEntry(entryDb);

            return _mapper.Map<EntryDTO>(entryDb);
        }

        public async Task<EntryDTO[]> GetEntriesByUser(Guid userId)
        {
            var entryDb = await _entryRepository.GetEntriesByUser(userId);
            return _mapper.Map<EntryDTO[]>(entryDb);
        }

        public async Task<CurrencyDTO[]> GetAllCurrencies()
        {
            var entryDb = await _entryRepository.GetAllCurrencies();
            return _mapper.Map<CurrencyDTO[]>(entryDb);
        }

        public async Task<EntryDTO> GetEntry(Guid entryId)
        {
            var entry = await _entryRepository.GetSingleEntry(entryId);
            return _mapper.Map<EntryDTO>(entry);
        }

        public async Task<EntryDTO[]> GetAllEntries()
        {
            var entryDb = await _entryRepository.GetAllEntries();
            return _mapper.Map<EntryDTO[]>(entryDb);
        }

        public async Task<EntryDTO[]> GetAllEntriesForBudget(Guid budgetId)
        {
            var entryDb = await _entryRepository.GetEntriesByBudget(budgetId);
            return _mapper.Map<EntryDTO[]>(entryDb);
        }
    }
}
