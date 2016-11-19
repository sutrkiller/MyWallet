using System;
using System.Data.Entity;
using System.Threading.Tasks;
using MyWallet.Entities.Models;
using Xunit;

namespace MyWallet.Entities.UnitTests.Repositories
{
    public class ConversionRatioRepositoryTests : BaseRepositoryTest
    {
        [Fact]
        public async Task SaveConversionRatio()
        {
            var testRatio = new ConversionRatio
            {
                Ratio = 26.45m,
                Date = DateTime.Now
            };

            var addedRatio = await ConversionRatioRepository.AddConversionRatio(testRatio);

            Assert.NotEqual(Guid.Empty, addedRatio.Id);
            Assert.Equal(testRatio.Ratio, addedRatio.Ratio);
            Assert.Equal(testRatio.Date, addedRatio.Date);

            var retrievedRatio = await ConversionRatioRepository.GetSingleConversionRatio(addedRatio.Id);
            Assert.Equal(addedRatio.Id, retrievedRatio.Id);
            Assert.Equal(addedRatio.Ratio, retrievedRatio.Ratio);
            Assert.Equal(addedRatio.Date, retrievedRatio.Date);
        }

        [Fact]
        public async Task GetAllConversionRatiosTest()
        {
            var allEntities = await ConversionRatioRepository.GetAllConversionRatios().ToArrayAsync();
            Assert.NotNull(allEntities);
            Assert.NotEmpty(allEntities);
            Assert.Equal(2, allEntities.Length);
            foreach (var entity in allEntities)
            {
                Assert.IsType<ConversionRatio>(entity);
            }
        }
    }
}
