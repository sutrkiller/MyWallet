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
    public class UserRepositoryTests
    {
        [Fact]
        public async Task SaveUser()
        {
            var connectionsOptionMock = Substitute.For<IOptions<ConnectionOptions>>();
            connectionsOptionMock.Value.Returns(new ConnectionOptions
            {
                ConnectionString = "" //insert connection string
            });

            var testUser = new User
            {
                Name = "Test",
                Email = "test@test.com"
            };

            var userRepository = new UserRepository(connectionsOptionMock);
            var addedUser = await userRepository.AddUser(testUser);

            Assert.NotEqual(Guid.Empty, addedUser.Id);
            Assert.Equal(testUser.Name, addedUser.Name);
            Assert.Equal(testUser.Email, addedUser.Email);

            var retrievedUser = await userRepository.GetSingleUser(addedUser.Id);
            Assert.Equal(addedUser.Id, retrievedUser.Id);
            Assert.Equal(addedUser.Name, retrievedUser.Name);
            Assert.Equal(addedUser.Email, retrievedUser.Email);

            var users = await userRepository.GetAllUsers();
            Assert.NotEmpty(users);
            foreach (var user in users)
            {
                Assert.NotEqual(Guid.Empty, user.Id);
                Assert.NotNull(user.Email);
            }
        }
    }
}
