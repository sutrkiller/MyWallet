﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyWallet.Entities.Repositories.Interfaces;
using MyWallet.Services.Services.Interfaces;
using System.Data.Entity;
using MyWallet.Services.Filters;
using ConversionRatio = MyWallet.Services.DataTransferModels.ConversionRatio;
using Currency = MyWallet.Services.DataTransferModels.Currency;
using Entry = MyWallet.Services.DataTransferModels.Entry;

namespace MyWallet.Services.Services
{
    internal class EntryService : IEntryService
    {
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

        public async Task<Entry> AddEntry(Entry entry, string userEmail, Guid conversionRatioId, ICollection<Guid> categoryIds, ICollection<Guid> budgetIds)
        {
            if (userEmail == null) throw new NullReferenceException("User is not logged in.");
            var dataAccessEntryModel = _mapper.Map<Entities.Models.Entry>(entry);

            var user = dataAccessEntryModel.User = await _userRepository.GetUserByEmail(userEmail);
            dataAccessEntryModel.User = user;

            dataAccessEntryModel.Categories = await _categoryRepository.GetCategoriesFromIds(categoryIds).ToArrayAsync();
            dataAccessEntryModel.Budgets = await _budgetRepository.GetBudgetsFromIds(budgetIds).ToArrayAsync();
            dataAccessEntryModel.ConversionRatio = await _conversionRatioRepository.GetSingleConversionRatio(conversionRatioId);
            dataAccessEntryModel = await _entryRepository.AddEntry(dataAccessEntryModel);
            return _mapper.Map<Entry>(dataAccessEntryModel);
        }

        public async Task EditEntry(Entry entry, string userEmail, Guid conversionRatioId, ICollection<Guid> categoryIds, ICollection<Guid> budgetIds)
        {
            if (userEmail == null) throw new NullReferenceException("User is not logged in.");
            var dataAccessEntryModel = _mapper.Map<Entities.Models.Entry>(entry);

            var user = dataAccessEntryModel.User = await _userRepository.GetUserByEmail(userEmail);
            dataAccessEntryModel.User = user;

            dataAccessEntryModel.Categories = await _categoryRepository.GetCategoriesFromIds(categoryIds).ToArrayAsync();
            dataAccessEntryModel.Budgets = await _budgetRepository.GetBudgetsFromIds(budgetIds).ToArrayAsync();
            dataAccessEntryModel.ConversionRatio = await _conversionRatioRepository.GetSingleConversionRatio(conversionRatioId);
            await _entryRepository.EditEntry(dataAccessEntryModel);
        }

        public async Task<Entry[]> GetEntriesByUser(Guid userId)
        {
            var entryDb = await _entryRepository.GetEntriesByUser(userId).ToArrayAsync();
            return _mapper.Map<Entry[]>(entryDb);
        }

        public async Task<Currency[]> GetAllCurrencies()
        {
            var entryDb = await _currencyRepository.GetAllCurrencies().ToArrayAsync();
            return _mapper.Map<Currency[]>(entryDb);
        }

        public async Task<Entry> GetEntry(Guid entryId)
        {
            var entry = await _entryRepository.GetSingleEntry(entryId);
            return _mapper.Map<Entry>(entry);
        }

        public async Task<Entry[]> GetAllEntries(EntriesFilter filter = null)
        {
            var entries = _entryRepository.GetAllEntries();

            if (filter != null)
            {
                if (filter.From.HasValue)
                {
                    entries = entries.Where(x => DbFunctions.TruncateTime(x.EntryTime) >= filter.From.Value);
                }

                if (filter.To.HasValue)
                {
                    entries = entries.Where(x => DbFunctions.TruncateTime(x.EntryTime) <= filter.To.Value);
                }
                if (filter.UserId.HasValue)
                {
                    entries = entries.Where(x => x.User.Id == filter.UserId.Value);
                }
                if (filter.BudgetId.HasValue)
                {
                    entries = entries.Where(x => x.Budgets.Any(b=>b.Id == filter.BudgetId.Value));
                }
                if (filter.CategoryId.HasValue)
                {
                    entries = entries.Where(x => x.Categories.Any(c => c.Id == filter.CategoryId.Value));
                }
            }

            return _mapper.Map<Entry[]>(await entries.ToArrayAsync());
        }

        public async Task<ConversionRatio[]> GetAllConversionRatios()
        {
            var conversionRatios = await _conversionRatioRepository.GetAllConversionRatios().ToArrayAsync();
            return _mapper.Map<ConversionRatio[]>(conversionRatios);
        }

        public async Task AddConversionRatios(IEnumerable<ConversionRatio> ratios)
        {
            if (ratios == null) return;
            var dbRatios = _mapper.Map<Entities.Models.ConversionRatio[]>(ratios);
            foreach (var ratio in dbRatios)
            {
                await _conversionRatioRepository.AddConversionRatio(ratio);
            }
        }
        public async Task<ConversionRatio[]> GetConversionRatiosForCurrency(Guid currencyId)
        {
            var conversionRatios = await _conversionRatioRepository.GetAllConversionRatios().Where(cr => cr.CurrencyFrom.Id == currencyId).ToArrayAsync();
            return _mapper.Map<ConversionRatio[]>(conversionRatios);
        }

        public async Task DeleteEntry(Guid id)
        {
            var entry = await _entryRepository.GetSingleEntry(id);
            await _entryRepository.DeleteEntry(entry);
        }

        public async Task<ConversionRatio> AddConversionRatio(Guid currencyId, string customRatioAmount, Guid customRatioCurrencyId)
        {
            var customRatio = new Entities.Models.ConversionRatio();
            var currency = await _currencyRepository.GetSingleCurrency(currencyId);
            var currency2 = await _currencyRepository.GetSingleCurrency(customRatioCurrencyId);
            decimal amount;
            decimal.TryParse(customRatioAmount, out amount);
            customRatio.Ratio = amount;
            customRatio.CurrencyFrom = currency;
            customRatio.CurrencyTo = currency2;
            customRatio.Type = "Custom";
            customRatio.Date = DateTime.Now;
            var dataAccessRatioModel = await _conversionRatioRepository.AddConversionRatio(customRatio);
            return _mapper.Map<ConversionRatio>(dataAccessRatioModel);
        }
    }
}

