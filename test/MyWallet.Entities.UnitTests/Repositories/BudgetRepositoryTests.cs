using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyWallet.Entities.Models;
using Xunit;

namespace MyWallet.Entities.UnitTests.Repositories
{
    public class BudgetRepositoryTests : BaseRepositoryTest
    {
        [Fact]
        public async Task SaveBudget()
        {
            var testGroup = new Group()
            {
                Name = "Family"
            };

            var cr = new ConversionRatio()
            {
                CurrencyFrom = new Currency() {Code = "CZK"},
                CurrencyTo = new Currency() {Code = "EUR"},
                Date = DateTime.Today,
                Ratio = 25.6m
            };

            var categories = new List<Category>() { new Category { Name = "Category", Description = "Description" } };

            var testBudget = new Budget
            {
                Amount = 100m,
                Name = "New Budget",
                Description = "Lunch",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7),
                Group = testGroup,
                ConversionRatio = cr,
                Categories = categories
            };

            var addedBudget = await BudgetRepository.AddBudget(testBudget);

            Assert.NotEqual(Guid.Empty, addedBudget.Id);
            Assert.Equal(testBudget.Amount, addedBudget.Amount);
            Assert.Equal(testBudget.Description, addedBudget.Description, StringComparer.CurrentCultureIgnoreCase);

            var retrievedBudget = await BudgetRepository.GetSingleBudget(addedBudget.Id);

            Assert.Equal(addedBudget.Id, retrievedBudget.Id);
            Assert.Equal(testBudget.Amount, retrievedBudget.Amount);
            Assert.Equal(testBudget.Description, retrievedBudget.Description);
        }

        [Fact]
        public async Task GetAllBudgetsTest()
        {
            var allBudgets = await BudgetRepository.GetAllBudgets().ToArrayAsync();
            Assert.NotNull(allBudgets);
            Assert.NotEmpty(allBudgets);
            Assert.Equal(2, allBudgets.Length);
            foreach (var budget in allBudgets)
            {
                Assert.IsType<Budget>(budget);
            }
        }
    }
}