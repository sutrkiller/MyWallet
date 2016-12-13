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
using Budget = MyWallet.Services.DataTransferModels.Budget;
using Category = MyWallet.Services.DataTransferModels.Category;
using Currency = MyWallet.Services.DataTransferModels.Currency;
using Entry = MyWallet.Services.DataTransferModels.Entry;
using Group = MyWallet.Services.DataTransferModels.Group;
using User = MyWallet.Services.DataTransferModels.User;

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
            Entities.Models.Category cat = new Entities.Models.Category() {Name = "Category1"};
            var budget = new Entities.Models.Budget()
            {
                Amount = 50m,
                Description = "desc",
                StartDate = date1,
                EndDate = date2,
                Name = "Budget",
                Categories = new []{cat}
            };
            var expected = new Budget()
            {
                Amount = 50m,
                Description = "desc",
                StartDate = date1,
                EndDate = date2,
                Name = "Budget"
            };
            var actual = GetMapper().Map<Budget>(budget);
            AssertPropertyEquals(expected, actual);
            Assert.NotEmpty(actual.Categories);
        }

        [Fact]
        public void Map_BudgetDTO_Budget()
        {
            var date1 = new DateTime(2016, 11, 16);
            var date2 = new DateTime(2016, 11, 17);
            Category cat = new Category() { Name = "Category1" };
            var budget = new Budget()
            {
                Amount = 50m,
                Description = "desc",
                StartDate = date1,
                EndDate = date2,
                Name = "Budget",
                Categories = new[] { cat }
            };
            var expected = new Entities.Models.Budget()
            {
                Amount = 50m,
                Description = "desc",
                StartDate = date1,
                EndDate = date2,
                Name = "Budget",
                
            };
            
            var actual = GetMapper().Map<Entities.Models.Budget>(budget);
            AssertPropertyEquals(expected, actual);
            Assert.NotEmpty(actual.Categories);
        }

        [Fact]
        public void Map_Category_CategoryDTO()
        {
            var category = new Entities.Models.Category()
            {
                Name = "Cat1",
                Description = "Description",
            };
            var expected = new Category()
            {
                Name = "Cat1",
                Description = "Description"
            };
            var actual = GetMapper().Map<Category>(category);
            AssertPropertyEquals(expected,actual);
        }

        [Fact]
        public void Map_CategoryDTO_Category()
        {
            var categoryDTO = new Category()
            {
                Name = "Cat1",
                Description = "Description"
            };
            var expected = new Entities.Models.Category()
            {
                Name = "Cat1",
                Description = "Description",
            };
            
            var actual = GetMapper().Map<Entities.Models.Category>(categoryDTO);
            AssertPropertyEquals(expected, actual);
        }

        [Fact]
        public void Map_Currency_CurrencyDTO()
        {
            var currency = new Entities.Models.Currency() {Code = "CZK"};
            var expected = new Currency() {Code = "CZK"};
            var actual = GetMapper().Map<Currency>(currency);
            AssertPropertyEquals(expected,actual);
        }

        [Fact]
        public void Map_CurrencyDTO_Currency()
        {
            var currencyDTO = new Currency() { Code = "CZK" };
            var expected = new Entities.Models.Currency() { Code = "CZK" };
            var actual = GetMapper().Map<Entities.Models.Currency>(currencyDTO);
            AssertPropertyEquals(expected, actual);
        }

        [Fact]
        public void Map_Entry_EntryDTO()
        {
            var entry = new Entities.Models.Entry()
            {
                Amount = 50m,
                Description = "Entry",
                EntryTime = new DateTime(2016,11,16)
            };
            var expected = new Entry()
            {
                Amount = 50m,
                Description = "Entry",
                EntryTime = new DateTime(2016, 11, 16)
            };
            var actual = GetMapper().Map<Entry>(entry);
            AssertPropertyEquals(expected,actual);
        }

        [Fact]
        public void Map_EntryDTO_Entry()
        {
            var entryDto = new Entry()
            {
                Amount = 50m,
                Description = "Entry",
                EntryTime = new DateTime(2016, 11, 16)
            };
            var expected = new Entities.Models.Entry()
            {
                Amount = 50m,
                Description = "Entry",
                EntryTime = new DateTime(2016, 11, 16)
            };
            var actual = GetMapper().Map<Entities.Models.Entry>(entryDto);
            AssertPropertyEquals(expected, actual);
        }

        [Fact]
        public void Map_Group_GroupDTO()
        {
            var group = new Entities.Models.Group() {Name = "Group"};
            var expected = new Group() {Name = "Group"};
            var actual = GetMapper().Map<Group>(group);

            AssertPropertyEquals(expected,actual);
        }

        [Fact]
        public void Map_GroupDTO_Group()
        {
            var groupDTO = new Group() { Name = "Group" };
            var expected = new Entities.Models.Group() { Name = "Group" };
            var actual = GetMapper().Map<Entities.Models.Group>(groupDTO);

            AssertPropertyEquals(expected, actual);
        }

        [Fact]
        public void Map_User_UserDTO()
        {
            var user = new Entities.Models.User() {Name = "Someone", Email = "email@provider.com"};
            var expected = new User() {Name = "Someone", Email = "email@provider.com"};
            var actual = GetMapper().Map<User>(user);

            AssertPropertyEquals(expected,actual);
        }

        [Fact]
        public void Map_UserDTO_User()
        {
            var userDTO = new User() { Name = "Someone", Email = "email@provider.com" };
            var expected = new Entities.Models.User() { Name = "Someone", Email = "email@provider.com" };
            var actual = GetMapper().Map<Entities.Models.User>(userDTO);

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
