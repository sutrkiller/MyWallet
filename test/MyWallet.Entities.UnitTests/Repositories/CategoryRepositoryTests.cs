using System;
using System.Data.Entity;
using System.Threading.Tasks;
using MyWallet.Entities.Models;
using Xunit;

namespace MyWallet.Entities.UnitTests.Repositories
{
    public class CategoryRepositoryTests : BaseRepositoryTest
    {
        [Fact]
        public async Task SaveCategory()
        {
            var testCategory = new Category
            {
                Name = "Test Category",
                Description = "Test"
            };

            var addedCategory = await CategoryRepository.AddCategory(testCategory);

            Assert.NotEqual(Guid.Empty, addedCategory.Id);
            Assert.Equal(testCategory.Name, addedCategory.Name);
            Assert.Equal(testCategory.Description, addedCategory.Description);

            var retrievedCategory = await CategoryRepository.GetSingleCategory(addedCategory.Id);
            Assert.Equal(addedCategory.Id, retrievedCategory.Id);
            Assert.Equal(addedCategory.Name, retrievedCategory.Name);
            Assert.Equal(addedCategory.Description, retrievedCategory.Description);
        }

        [Fact]
        public async Task GetAllCategories()
        {
            var categories = await CategoryRepository.GetAllCategories().ToArrayAsync();
            Assert.NotNull(categories);
            Assert.NotEmpty(categories);
            Assert.Equal(2, categories.Length);
            foreach (var category in categories)
            {
                Assert.IsType<Category>(category);
            }
        }
    }
}
