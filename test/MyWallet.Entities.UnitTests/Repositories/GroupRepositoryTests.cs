using System;
using System.Threading.Tasks;
using MyWallet.Entities.Models;
using Xunit;

namespace MyWallet.Entities.UnitTests.Repositories
{
    public class GroupRepositoryTests : BaseRepositoryTest
    {
        [Fact]
        public async Task SaveGroup()
        {
            var testGroup = new Group
            {
                Name = "Test Group"
            };

            var addedGroup = await GroupRepository.AddGroup(testGroup);

            Assert.NotEqual(Guid.Empty, addedGroup.Id);
            Assert.Equal(testGroup.Name, addedGroup.Name);

            var retrievedGroup = await GroupRepository.GetSingleGroup(addedGroup.Id);
            Assert.Equal(addedGroup.Id, retrievedGroup.Id);
            Assert.Equal(addedGroup.Name, retrievedGroup.Name);
        }

        [Fact]
        public async Task GetAllGroupsTest()
        {
            var allEntities = await GroupRepository.GetAllGroups();
            Assert.NotNull(allEntities);
            Assert.NotEmpty(allEntities);
            Assert.Equal(2, allEntities.Length);
            foreach (var entity in allEntities)
            {
                Assert.IsType<Group>(entity);
            }
        }
    }
}