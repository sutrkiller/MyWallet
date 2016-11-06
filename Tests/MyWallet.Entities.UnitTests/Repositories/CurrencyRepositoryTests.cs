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
    public class CurrencyRepositoryTests
    {
        [Fact]
        public async Task SaveCurrency()
        {
            var connectionsOptionMock = Substitute.For<IOptions<ConnectionOptions>>();
            connectionsOptionMock.Value.Returns(new ConnectionOptions
            {
                ConnectionString = "" //insert connection string
            });

            var testCurrency = new Currency
            {
                Code = "Test"
            };

            var currencyRepository = new CurrencyRepository(connectionsOptionMock);
            var addedCurrency = await currencyRepository.AddCurrency(testCurrency);

            Assert.NotEqual(Guid.Empty, addedCurrency.Id);
            Assert.Equal(testCurrency.Code, addedCurrency.Code);

            var retrievedCurrency = await currencyRepository.GetSingleCurrency(addedCurrency.Id);
            Assert.Equal(addedCurrency.Id, retrievedCurrency.Id);
            Assert.Equal(addedCurrency.Code, retrievedCurrency.Code);

            var currencies = await currencyRepository.GetAllCurrencies();
            Assert.NotEmpty(currencies);
            foreach (var currency in currencies)
            {
                Assert.NotEqual(Guid.Empty, currency.Id);
                Assert.NotNull(currency.Code);
            }
        }
    }
}
