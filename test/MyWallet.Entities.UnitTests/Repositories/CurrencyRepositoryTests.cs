using System;
using System.Threading.Tasks;
using MyWallet.Entities.Models;
using Xunit;

namespace MyWallet.Entities.UnitTests.Repositories
{
    public class CurrencyRepositoryTests : BaseRepositoryTest
    {
        [Fact]
        public async Task SaveCurrency()
        {
            var testCurrency = new Currency
            {
                Code = "Test"
            };

            var addedCurrency = await CurrencyRepository.AddCurrency(testCurrency);

            Assert.NotEqual(Guid.Empty, addedCurrency.Id);
            Assert.Equal(testCurrency.Code, addedCurrency.Code);

            var retrievedCurrency = await CurrencyRepository.GetSingleCurrency(addedCurrency.Id);
            Assert.Equal(addedCurrency.Id, retrievedCurrency.Id);
            Assert.Equal(addedCurrency.Code, retrievedCurrency.Code);
        }

        [Fact]
        public async Task GetAllCurrenciesTest()
        {
            var allEntities = await CurrencyRepository.GetAllCurrencies();
            Assert.NotNull(allEntities);
            Assert.NotEmpty(allEntities);
            Assert.Equal(2, allEntities.Length);
            foreach (var entity in allEntities)
            {
                Assert.IsType<Currency>(entity);
            }
        }
    }
}