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
    public class ConversionRatioRepositoryTests
    {
        [Fact]
        public async Task SaveConversionRatio()
        {
            var connectionsOptionMock = Substitute.For<IOptions<ConnectionOptions>>();
            connectionsOptionMock.Value.Returns(new ConnectionOptions
            {
                ConnectionString = "" //insert connection string
            });

            var testRatio = new ConversionRatio
            {
                Ratio = 26.45m,
                Date = DateTime.Now
            };

            var conversionRatioRepository = new ConversionRatioRepository(connectionsOptionMock);
            var addedRatio = await conversionRatioRepository.AddConversionRatio(testRatio);

            Assert.NotEqual(Guid.Empty, addedRatio.Id);
            Assert.Equal(testRatio.Ratio, addedRatio.Ratio);
            Assert.Equal(testRatio.Date, addedRatio.Date);

            var retrievedRatio = await conversionRatioRepository.GetSingleConversionRatio(addedRatio.Id);
            Assert.Equal(addedRatio.Id, retrievedRatio.Id);
            Assert.Equal(addedRatio.Ratio, retrievedRatio.Ratio);
            Assert.Equal(addedRatio.Date, retrievedRatio.Date);

            var ratios = await conversionRatioRepository.GetAllConversionRatios();
            Assert.NotEmpty(ratios);
            foreach (var ratio in ratios)
            {
                Assert.NotEqual(Guid.Empty, ratio.Id);
                Assert.NotEqual(0m, ratio.Ratio);
                Assert.NotEqual(DateTime.MinValue, ratio.Date);
            }
        }
    }
}
