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
using System.Data.Entity;

namespace MyWallet.Services.Services
{
    public class EntryService : IEntryService
    {
        private const string MainCurrency = "EUR";

        private readonly IEntryRepository _entryRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IBudgetRepository _budgetRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IConversionRatioRepository _conversionRatioRepository;
        private readonly IMapper _mapper;

        public EntryService(
            IEntryRepository entryRepository,
            IMapper mapper, ICurrencyRepository currencyRepository, IBudgetRepository budgetRepository, IUserRepository userRepository, ICategoryRepository categoryRepository, IConversionRatioRepository conversionRatioRepository)
        {
            _entryRepository = entryRepository;
            _mapper = mapper;
            _currencyRepository = currencyRepository;
            _budgetRepository = budgetRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _conversionRatioRepository = conversionRatioRepository;
        }

        public async Task<EntryDTO> AddEntry(EntryDTO entry, string userEmail, Guid conversionRatioId, ICollection<Guid> categoryIds, ICollection<Guid> budgetIds)
        {
            if (userEmail==null) throw new NullReferenceException("User is not logged in.");
            //todo: download conversion from net
            var dataAccessEntryModel = _mapper.Map<Entry>(entry);

            var user = dataAccessEntryModel.User = await _userRepository.GetUserByEmail(userEmail);
            dataAccessEntryModel.User = user;
                       
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
            var entryDb = await _currencyRepository.GetAllCurrencies();
            return _mapper.Map<CurrencyDTO[]>(entryDb);
        }

        public async Task<EntryDTO> GetEntry(Guid entryId)
        {
            var entry = await _entryRepository.GetSingleEntry(entryId);
            return _mapper.Map<EntryDTO>(entry);
        }

        public async Task<EntryDTO[]> GetAllEntries()
        {
            //TODO: change this to filter entries
            var entryDb = await _entryRepository.GetAllEntries().ToArrayAsync();
            return _mapper.Map<EntryDTO[]>(entryDb);
        }

        public async Task<ConversionRatioDTO[]> GetAllConversionRatios()
      {
          var conversionRatios = await _conversionRatioRepository.GetAllConversionRatios().ToArrayAsync();
          return _mapper.Map<ConversionRatioDTO[]>(conversionRatios);
      }

    public async Task<EntryDTO[]> GetAllEntriesForBudget(Guid budgetId)
        {
            //TODO: change this later
            await Task.Delay(0);
            var entryDb = _budgetRepository.GetAllBudgets().SingleOrDefault(x => x.Id.Equals(budgetId))?.Entries;
            return _mapper.Map<EntryDTO[]>(entryDb);
        }

        public async Task AddConversionRatios(IEnumerable<ConversionRatioDTO> ratios)
        {
            if (ratios == null) return;
            var dbRatios = _mapper.Map<ConversionRatio[]>(ratios);
            foreach (var ratio in dbRatios)
            {
                await _conversionRatioRepository.AddConversionRatio(ratio);
            }
        }

        public async Task DeleteEntry(Guid id)
        {
            var entry = await _entryRepository.GetSingleEntry(id);
            await _entryRepository.DeleteEntry(entry);
        }
    }
}
