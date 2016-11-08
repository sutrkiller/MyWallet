using Microsoft.Extensions.Options;
using MyWallet.Entities.Configuration;
using MyWallet.Entities.DataAccessModels;
using MyWallet.Entities.Repositories;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                EntryDateTime = DateTime.Now
            };

            var addedEntry = await EntryRepository.AddEntry(testEntry);

            Assert.NotEqual(Guid.Empty, addedEntry.Id);
            Assert.Equal(testEntry.Amount, addedEntry.Amount);
            Assert.Equal(testEntry.Description, addedEntry.Description);
            Assert.Equal(testEntry.EntryDateTime, addedEntry.EntryDateTime);

            var retrievedEntry = await EntryRepository.GetSingleEntry(addedEntry.Id);
            Assert.Equal(addedEntry.Id, retrievedEntry.Id);
            Assert.Equal(addedEntry.Amount, retrievedEntry.Amount);
            Assert.Equal(addedEntry.Description, retrievedEntry.Description);
            Assert.Equal(addedEntry.EntryDateTime, retrievedEntry.EntryDateTime);
        }

        [Fact]
        public async Task GetAllEntriesTest()
        {
            var allEntities = await EntryRepository.GetAllEntries();
            Assert.NotNull(allEntities);
            Assert.NotEmpty(allEntities);
            Assert.Equal(2, allEntities.Length);
            foreach (var entity in allEntities)
            {
                Assert.IsType<Entry>(entity);
            }
        }

        [Fact]
        public async Task SaveEntryToBudget()
        {
            var testEntry = new Entry
            {
                Amount = 105m,
                Description = "Entry with budget test.",
                EntryDateTime = DateTime.Now
            };

            var testBudget = new Budget
            {
                Amount = 500m,
                Description = "Budget with entry test."
            };
            var addedBudget = await BudgetRepository.AddBudget(testBudget);
            var addedEntry = await EntryRepository.AddEntryToBudget(testEntry, addedBudget);

            Assert.NotEmpty(addedEntry.Budgets);
            foreach (var budget in addedEntry.Budgets)
            {
                Assert.NotEqual(Guid.Empty, budget.Id);
                Assert.NotEqual(0m, budget.Amount);
            }
        }
    }
}
