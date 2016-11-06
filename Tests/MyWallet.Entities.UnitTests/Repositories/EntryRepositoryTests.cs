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
    public class EntryRepositoryTests
    {
        [Fact]
        public async Task SaveEntry()
        {
            var connectionsOptionMock = Substitute.For<IOptions<ConnectionOptions>>();
            connectionsOptionMock.Value.Returns(new ConnectionOptions
            {
                ConnectionString = "" //insert connection string
            });

            var testEntry = new Entry
            {
                Amount = 105m,
                Description = "Test",
                EntryDateTime = DateTime.Now
            };

            var currencyRepository = new EntryRepository(connectionsOptionMock);
            var addedEntry = await currencyRepository.AddEntry(testEntry);

            Assert.NotEqual(Guid.Empty, addedEntry.Id);
            Assert.Equal(testEntry.Amount, addedEntry.Amount);
            Assert.Equal(testEntry.Description, addedEntry.Description);
            Assert.Equal(testEntry.EntryDateTime, addedEntry.EntryDateTime);

            var retrievedEntry = await currencyRepository.GetSingleEntry(addedEntry.Id);
            Assert.Equal(addedEntry.Id, retrievedEntry.Id);
            Assert.Equal(addedEntry.Amount, retrievedEntry.Amount);
            Assert.Equal(addedEntry.Description, retrievedEntry.Description);
            Assert.Equal(addedEntry.EntryDateTime, retrievedEntry.EntryDateTime);

            var entries = await currencyRepository.GetAllEntries();
            Assert.NotEmpty(entries);
            foreach (var entry in entries)
            {
                Assert.NotEqual(Guid.Empty, entry.Id);
                Assert.NotEqual(0m, entry.Amount);
                Assert.NotEqual(DateTime.MinValue, entry.EntryDateTime);
            }
        }

        [Fact]
        public async Task SaveEntryToBudget()
        {
            var connectionsOptionMock = Substitute.For<IOptions<ConnectionOptions>>();
            connectionsOptionMock.Value.Returns(new ConnectionOptions
            {
                ConnectionString = "" //insert connection string
            });

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

            var currencyRepository = new EntryRepository(connectionsOptionMock);
            var budgetRepository = new BudgetRepository(connectionsOptionMock);
            var addedBudget = await budgetRepository.AddBudget(testBudget);
            var addedEntry = await currencyRepository.AddEntryToBudget(testEntry, addedBudget);

            Assert.NotEmpty(addedEntry.Budgets);
            foreach (var budget in addedEntry.Budgets)
            {
                Assert.NotEqual(Guid.Empty, budget.Id);
                Assert.NotEqual(0m, budget.Amount);
            }
        }
    }
}
