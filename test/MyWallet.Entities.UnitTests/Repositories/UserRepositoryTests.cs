using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWallet.Entities.Models;
using Xunit;

namespace MyWallet.Entities.UnitTests.Repositories
{
    public class UserRepositoryTests : BaseRepositoryTest
    {
        [Fact]
        public async Task SaveUser()
        {
            var testUser = new User
            {
                Name = "Test",
                Email = "test@test.com"
            };

            var addedUser = await UserRepository.AddUser(testUser);

            Assert.NotEqual(Guid.Empty, addedUser.Id);
            Assert.Equal(testUser.Name, addedUser.Name);
            Assert.Equal(testUser.Email, addedUser.Email);

            var retrievedUser = await UserRepository.GetSingleUser(addedUser.Id);
            Assert.Equal(addedUser.Id, retrievedUser.Id);
            Assert.Equal(addedUser.Name, retrievedUser.Name);
            Assert.Equal(addedUser.Email, retrievedUser.Email);
        }

        [Fact]
        public async Task GetAllUsersTest()
        {
            var allEntities = await UserRepository.GetAllUsers();
            Assert.NotNull(allEntities);
            Assert.NotEmpty(allEntities);
            Assert.Equal(2, allEntities.Length);
            foreach (var entity in allEntities)
            {
                Assert.IsType<User>(entity);
            }
        }
    }
}
