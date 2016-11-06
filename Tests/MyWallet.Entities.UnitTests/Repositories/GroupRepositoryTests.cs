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
    public class GroupRepositoryTests
    {
        [Fact]
        public async Task SaveGroup()
        {
            var connectionsOptionMock = Substitute.For<IOptions<ConnectionOptions>>();
            connectionsOptionMock.Value.Returns(new ConnectionOptions
            {
                ConnectionString = "" //insert connection string
            });

            var testGroup = new Group
            {
                Name = "Test Group"
            };

            var groupRepository = new GroupRepository(connectionsOptionMock);
            var addedGroup = await groupRepository.AddGroup(testGroup);

            Assert.NotEqual(Guid.Empty, addedGroup.Id);
            Assert.Equal(testGroup.Name, addedGroup.Name);

            var retrievedGroup = await groupRepository.GetSingleGroup(addedGroup.Id);
            Assert.Equal(addedGroup.Id, retrievedGroup.Id);
            Assert.Equal(addedGroup.Name, retrievedGroup.Name);

            var groups = await groupRepository.GetAllGroups();
            Assert.NotEmpty(groups);
            foreach (var group in groups)
            {
                Assert.NotEqual(Guid.Empty, group.Id);
                Assert.NotNull(group.Name);
            }
        }
    }
}
