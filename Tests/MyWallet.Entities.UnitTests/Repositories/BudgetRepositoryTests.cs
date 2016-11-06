using Microsoft.Extensions.Options;
using MyWallet.Entities.Configuration;
using MyWallet.Entities.DataAccessModels;
using MyWallet.Entities.Repositories;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyWallet.Entities.UnitTests.Repositories
{
    public class BudgetRepositoryTests
    {
        [Fact]
        public async Task SaveBudget()
        {
            var connectionsOptionMock = Substitute.For<IOptions<ConnectionOptions>>();
            connectionsOptionMock.Value.Returns(new ConnectionOptions
            {
                ConnectionString = "" //insert connection string
            });

            var testBudget = new Budget
            {
                Amount = 100m,
                Description = "Lunch"
            };

            var budgetRepository = new BudgetRepository(connectionsOptionMock);
            var addedBudget = await budgetRepository.AddBudget(testBudget);

            Assert.NotEqual(Guid.Empty, addedBudget.Id);
            Assert.Equal(testBudget.Amount, addedBudget.Amount);
            Assert.Equal(testBudget.Description, addedBudget.Description, StringComparer.CurrentCultureIgnoreCase);

            var retrivedBudget = await budgetRepository.GetSingleBudget(addedBudget.Id);

            Assert.Equal(addedBudget.Id, retrivedBudget.Id);
            Assert.Equal(testBudget.Amount, retrivedBudget.Amount);
            Assert.Equal(testBudget.Description, retrivedBudget.Description);

            var budgets = await budgetRepository.GetAllBudgets();
            Assert.NotEmpty(budgets);
            foreach (var budget in budgets)
            {
                Assert.NotEqual(Guid.Empty, budget.Id);
                Assert.NotEqual(0m, budget.Amount);
            }
        }
    }
}
