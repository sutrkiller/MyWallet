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
    public class CategoryRepositoryTests
    {
        [Fact]
        public async Task SaveCategory()
        {
            var connectionsOptionMock = Substitute.For<IOptions<ConnectionOptions>>();
            connectionsOptionMock.Value.Returns(new ConnectionOptions
            {
                ConnectionString = "" //insert connection string
            });

            var testCategory = new Category
            {
                Name = "Test Category",
                Description = "Test"
            };

            var categoryRepository = new CategoryRepository(connectionsOptionMock);
            var addedCategory = await categoryRepository.AddCategory(testCategory);

            Assert.NotEqual(Guid.Empty, addedCategory.Id);
            Assert.Equal(testCategory.Name, addedCategory.Name);
            Assert.Equal(testCategory.Description, addedCategory.Description);

            var retrievedCategory = await categoryRepository.GetSingleCategory(addedCategory.Id);
            Assert.Equal(addedCategory.Id, retrievedCategory.Id);
            Assert.Equal(addedCategory.Name, retrievedCategory.Name);
            Assert.Equal(addedCategory.Description, retrievedCategory.Description);

            var categories = await categoryRepository.GetAllCategories();
            Assert.NotEmpty(categories);
            foreach (var category in categories)
            {
                Assert.NotEqual(Guid.Empty, category.Id);
                Assert.NotNull(category.Name);
            }
        }
    }
}
