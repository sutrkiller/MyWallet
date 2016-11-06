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
    public class TimePeriodRepositoryTests
    {
        [Fact]
        public async Task SaveTimePeriod()
        {
            var connectionsOptionMock = Substitute.For<IOptions<ConnectionOptions>>();
            connectionsOptionMock.Value.Returns(new ConnectionOptions
            {
                ConnectionString = "" //insert connection string
            });

            var testTimePeriod = new TimePeriod
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(2)
            };

            var timePeriodRepository = new TimePeriodRepository(connectionsOptionMock);
            var addedTimePeriod = await timePeriodRepository.AddTimePeriod(testTimePeriod);

            Assert.NotEqual(Guid.Empty, addedTimePeriod.Id);
            Assert.Equal(testTimePeriod.StartDate, addedTimePeriod.StartDate);
            Assert.Equal(testTimePeriod.EndDate, addedTimePeriod.EndDate);

            var retrievedTimePeriod = await timePeriodRepository.GetSingleTimePeriod(addedTimePeriod.Id);
            Assert.Equal(addedTimePeriod.Id, retrievedTimePeriod.Id);
            Assert.Equal(addedTimePeriod.StartDate, retrievedTimePeriod.StartDate);
            Assert.Equal(addedTimePeriod.EndDate, retrievedTimePeriod.EndDate);

            var timePeriods = await timePeriodRepository.GetAllTimePeriods();
            Assert.NotEmpty(timePeriods);
            foreach (var timePeriod in timePeriods)
            {
                Assert.NotEqual(Guid.Empty, timePeriod.Id);
                Assert.NotEqual(DateTime.MinValue, timePeriod.StartDate);
                Assert.NotEqual(DateTime.MinValue, timePeriod.EndDate);
            }
        }
    }
}
