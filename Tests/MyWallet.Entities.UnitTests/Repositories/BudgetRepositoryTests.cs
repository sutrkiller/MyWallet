using MyWallet.Entities.DataAccessModels;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyWallet.Entities.UnitTests.Repositories
{

    public class BudgetRepositoryTests : BaseRepositoryTest
    {

        [Fact]
        public async Task SaveBudget()
        {
            var testBudget = new Budget
            {
                Amount = 100m,
                Description = "Lunch"
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
            var allBudgets = await BudgetRepository.GetAllBudgets();
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
