using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using MyWallet.Entities.Models;
using MyWallet.Services.Configuration;
using MyWallet.Services.DataTransferModels;
using Xunit;

namespace MyWallet.Services.UnitTests.MappingsTests
{
    public class MapperConfigurationTests
    {
        [Fact]
        public void AsserMapperConfiguration()
        {
            GetMapperConfiguration().AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_Budget_BudgetDTO()
        {
            var date1 = new DateTime(2016, 11, 16);
            var date2 = new DateTime(2016, 11, 17);
            Category cat = new Category() {Name = "Category1"};
            var budget = new Budget()
            {
                Amount = 50m,
                Description = "desc",
                StartDate = date1,
                EndDate = date2,
                Name = "Budget",
                Categories = new []{cat}
            };
            var expected = new BudgetDTO()
            {
                Amount = 50m,
                Description = "desc",
                StartDate = date1,
                EndDate = date2,
                Name = "Budget"
            };
            var actual = GetMapper().Map<BudgetDTO>(budget);
            AssertPropertyEquals(expected, actual);
            Assert.NotEmpty(actual.Categories);
        }

        [Fact]
        public void Map_BudgetDTO_Budget()
        {
            var date1 = new DateTime(2016, 11, 16);
            var date2 = new DateTime(2016, 11, 17);
            CategoryDTO cat = new CategoryDTO() { Name = "Category1" };
            var budget = new BudgetDTO()
            {
                Amount = 50m,
                Description = "desc",
                StartDate = date1,
                EndDate = date2,
                Name = "Budget",
                Categories = new[] { cat }
            };
            var expected = new Budget()
            {
                Amount = 50m,
                Description = "desc",
                StartDate = date1,
                EndDate = date2,
                Name = "Budget",
                
            };
            
            var actual = GetMapper().Map<Budget>(budget);
            AssertPropertyEquals(expected, actual);
            Assert.NotEmpty(actual.Categories);
        }

        [Fact]
        public void Map_Category_CategoryDTO()
        {
            var category = new Category()
            {
                Name = "Cat1",
                Description = "Description",
            };
            var expected = new CategoryDTO()
            {
                Name = "Cat1",
                Description = "Description"
            };
            var actual = GetMapper().Map<CategoryDTO>(category);
            AssertPropertyEquals(expected,actual);
        }

        [Fact]
        public void Map_CategoryDTO_Category()
        {
            var categoryDTO = new CategoryDTO()
            {
                Name = "Cat1",
                Description = "Description"
            };
            var expected = new Category()
            {
                Name = "Cat1",
                Description = "Description",
            };
            
            var actual = GetMapper().Map<Category>(categoryDTO);
            AssertPropertyEquals(expected, actual);
        }

        [Fact]
        public void Map_Currency_CurrencyDTO()
        {
            var currency = new Currency() {Code = "CZK"};
            var expected = new CurrencyDTO() {Code = "CZK"};
            var actual = GetMapper().Map<CurrencyDTO>(currency);
            AssertPropertyEquals(expected,actual);
        }

        [Fact]
        public void Map_CurrencyDTO_Currency()
        {
            var currencyDTO = new CurrencyDTO() { Code = "CZK" };
            var expected = new Currency() { Code = "CZK" };
            var actual = GetMapper().Map<Currency>(currencyDTO);
            AssertPropertyEquals(expected, actual);
        }

        [Fact]
        public void Map_Entry_EntryDTO()
        {
            var entry = new Entry()
            {
                Amount = 50m,
                Description = "Entry",
                EntryTime = new DateTime(2016,11,16)
            };
            var expected = new EntryDTO()
            {
                Amount = 50m,
                Description = "Entry",
                EntryTime = new DateTime(2016, 11, 16)
            };
            var actual = GetMapper().Map<EntryDTO>(entry);
            AssertPropertyEquals(expected,actual);
        }

        [Fact]
        public void Map_EntryDTO_Entry()
        {
            var entryDto = new EntryDTO()
            {
                Amount = 50m,
                Description = "Entry",
                EntryTime = new DateTime(2016, 11, 16)
            };
            var expected = new Entry()
            {
                Amount = 50m,
                Description = "Entry",
                EntryTime = new DateTime(2016, 11, 16)
            };
            var actual = GetMapper().Map<Entry>(entryDto);
            AssertPropertyEquals(expected, actual);
        }

        [Fact]
        public void Map_Group_GroupDTO()
        {
            var group = new Group() {Name = "Group"};
            var expected = new GroupDTO() {Name = "Group"};
            var actual = GetMapper().Map<GroupDTO>(group);

            AssertPropertyEquals(expected,actual);
        }

        [Fact]
        public void Map_GroupDTO_Group()
        {
            var groupDTO = new GroupDTO() { Name = "Group" };
            var expected = new Group() { Name = "Group" };
            var actual = GetMapper().Map<Group>(groupDTO);

            AssertPropertyEquals(expected, actual);
        }

        [Fact]
        public void Map_User_UserDTO()
        {
            var user = new User() {Name = "Someone", Email = "email@provider.com"};
            var expected = new UserDTO() {Name = "Someone", Email = "email@provider.com"};
            var actual = GetMapper().Map<UserDTO>(user);

            AssertPropertyEquals(expected,actual);
        }

        [Fact]
        public void Map_UserDTO_User()
        {
            var userDTO = new UserDTO() { Name = "Someone", Email = "email@provider.com" };
            var expected = new User() { Name = "Someone", Email = "email@provider.com" };
            var actual = GetMapper().Map<User>(userDTO);

            AssertPropertyEquals(expected, actual);
        }

        /// <summary>
        /// Expects equality of (presumably scaler) values of each public property of <typeparamref name="TType"/>.
        /// </summary>
        private static void AssertPropertyEquals<TType>(TType expectedDataAccessGameModel, TType actualDataAccessGameModel)
        {
            foreach (var property in typeof(TType).GetProperties(BindingFlags.Public | BindingFlags.GetProperty))
            {
                Assert.Equal(property.GetValue(expectedDataAccessGameModel), property.GetValue(actualDataAccessGameModel));
            }
        }

        private static MapperConfiguration GetMapperConfiguration()
            => new MapperConfiguration(ServicesMapperConfiguration.InitializeMappings);

        private static IMapper GetMapper() => GetMapperConfiguration().CreateMapper();
    }
}
