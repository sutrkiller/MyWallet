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
    public class TimePeriodRepositoryTests : BaseRepositoryTest
    {
        [Fact]
        public async Task SaveTimePeriod()
        {
            var testTimePeriod = new TimePeriod
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(2)
            };

            var addedTimePeriod = await TimePeriodRepository.AddTimePeriod(testTimePeriod);

            Assert.NotEqual(Guid.Empty, addedTimePeriod.Id);
            Assert.Equal(testTimePeriod.StartDate, addedTimePeriod.StartDate);
            Assert.Equal(testTimePeriod.EndDate, addedTimePeriod.EndDate);

            var retrievedTimePeriod = await TimePeriodRepository.GetSingleTimePeriod(addedTimePeriod.Id);
            Assert.Equal(addedTimePeriod.Id, retrievedTimePeriod.Id);
            Assert.Equal(addedTimePeriod.StartDate, retrievedTimePeriod.StartDate);
            Assert.Equal(addedTimePeriod.EndDate, retrievedTimePeriod.EndDate);
        }

        [Fact]
        public async Task GetAllTimePeriodsTest()
        {
            var allEntities = await TimePeriodRepository.GetAllTimePeriods();
            Assert.NotNull(allEntities);
            Assert.NotEmpty(allEntities);
            Assert.Equal(2, allEntities.Length);
            foreach (var entity in allEntities)
            {
                Assert.IsType<TimePeriod>(entity);
            }
        }
    }
}
