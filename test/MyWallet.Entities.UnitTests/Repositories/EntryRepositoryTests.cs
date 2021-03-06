﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using MyWallet.Entities.Models;
using Xunit;

namespace MyWallet.Entities.UnitTests.Repositories
{
    public class EntryRepositoryTests : BaseRepositoryTest
    {
        [Fact]
        public async Task SaveEntry()
        {
            var testEntry = new Entry
            {
                Amount = 105m,
                Description = "Test",
                EntryTime = DateTime.Now,
                ConversionRatio = new ConversionRatio() { Date = DateTime.Today,Ratio = 15m,Type = "Testing",CurrencyTo = new Currency() { Code = "CZK"},CurrencyFrom = new Currency() { Code = "EUR"} },
                User = new User() { Email = "email@email.com",Name = "Name", PreferredCurrency = new Currency() { Code = "EUR"} },
                Categories = new List<Category>() { new Category() {Name = "Cat1"} }
            };

            var addedEntry = await EntryRepository.AddEntry(testEntry);

            Assert.NotEqual(Guid.Empty, addedEntry.Id);
            Assert.Equal(testEntry.Amount, addedEntry.Amount);
            Assert.Equal(testEntry.Description, addedEntry.Description);
            Assert.Equal(testEntry.EntryTime, addedEntry.EntryTime);

            var retrievedEntry = await EntryRepository.GetSingleEntry(addedEntry.Id);
            Assert.Equal(addedEntry.Id, retrievedEntry.Id);
            Assert.Equal(addedEntry.Amount, retrievedEntry.Amount);
            Assert.Equal(addedEntry.Description, retrievedEntry.Description);
            Assert.Equal(addedEntry.EntryTime, retrievedEntry.EntryTime);
        }

        [Fact]
        public async Task GetAllEntriesTest()
        {
            var allEntities = await EntryRepository.GetAllEntries().ToArrayAsync();
            Assert.NotNull(allEntities);
            Assert.NotEmpty(allEntities);
            Assert.Equal(2, allEntities.Length);
            foreach (var entity in allEntities)
            {
                Assert.IsType<Entry>(entity);
            }
        }
    }
}