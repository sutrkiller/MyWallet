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
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IBudgetRepository _budgetRepository;
        private readonly IConversionRatioRepository _conversionRatioRepository;
        private readonly IMapper _mapper;
        private readonly string MAINCURRENCY = "EUR";
        public EntryService(
            IEntryRepository entryRepository,
            IUserRepository userRepository,
            ICategoryRepository categoryRepository,
            ICurrencyRepository currencyRepository,
            IBudgetRepository budgetRepository,
            IConversionRatioRepository conversionRatioRepository,
            IMapper mapper
            )
        {
            _entryRepository = entryRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _currencyRepository = currencyRepository;
            _conversionRatioRepository = conversionRatioRepository;
            _budgetRepository = budgetRepository;
            _mapper = mapper;
        }

        public async Task<EntryDTO> AddEntry(EntryDTO entry, Guid userId, Guid conversionRatioId, ICollection<Guid> categoryIds, ICollection<Guid> budgetIds)
        {
            //todo: download conversion from net
            var dataAccessEntryModel = _mapper.Map<Entry>(entry);
            var users = await _userRepository.GetAllUsers();
            dataAccessEntryModel.User = users.FirstOrDefault();
            //az bude funkcni
            //dataAccessEntryModel.User = await _userRepository.GetSingleUser(userId);
            dataAccessEntryModel.Categories = await _categoryRepository.GetCategoriesFromIds(categoryIds);
            dataAccessEntryModel.Budgets = await _budgetRepository.GetBudgetsFromIds(budgetIds);
            dataAccessEntryModel.ConversionRatio = await _conversionRatioRepository.GetSingleConversionRatio(conversionRatioId);
            dataAccessEntryModel = await _entryRepository.AddEntry(dataAccessEntryModel);
            return _mapper.Map<EntryDTO>(dataAccessEntryModel);
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
        public async Task<ConversionRatioDTO[]> GetAllConversionRatios()
        {
            var conversionRatios = await _conversionRatioRepository.GetAllConversionRatios();
            return _mapper.Map<ConversionRatioDTO[]>(conversionRatios);
        }
        public async Task<EntryDTO[]> GetAllEntriesForBudget(Guid budgetId)
        {
            var entryDb = await _entryRepository.GetEntriesByBudget(budgetId);
            return _mapper.Map<EntryDTO[]>(entryDb);
        }
    }
}
