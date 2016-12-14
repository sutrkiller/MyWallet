using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using MyWallet.Entities.Contexts;
using MyWallet.Entities.Models;
using MyWallet.Entities.Repositories;
using NSubstitute;
using NSubstitute.Core;

namespace MyWallet.Entities.UnitTests.Repositories
{
    public abstract class BaseRepositoryTest
    {
        internal readonly BudgetRepository BudgetRepository;
        internal readonly CategoryRepository CategoryRepository;
        internal readonly ConversionRatioRepository ConversionRatioRepository;
        internal readonly CurrencyRepository CurrencyRepository;
        internal readonly EntryRepository EntryRepository;
        internal readonly GroupRepository GroupRepository;
        internal readonly UserRepository UserRepository;


        /// <summary>
        /// Creates substitute for a <see cref="DbSet{TEntity}"/> with database replaced with an in-memory structure represented by <paramref name="data"/>.
        /// Can be used for querying and addition, including async operations.
        /// </summary>
        /// <typeparam name="TType">Type of data and <see cref="DbSet{TEntity}"/> to substitute</typeparam>
        /// <param name="data">Initial content of "database"</param>
        /// <returns>Queryable that can be used as <see cref="DbSet"/> substitute.</returns>
        private static IQueryable<TType> SubstituteQueryable<TType>(ICollection<TType> data)
            where TType : ModelBase
        {
            var queryableData = data.AsQueryable();
            var queryableSubstitute = Substitute.For<IQueryable<TType>, IDbAsyncEnumerable<TType>, DbSet<TType>>();

            // Mock queryable
            queryableSubstitute.Provider.Returns(new TestDbAsyncQueryProvider<TType>(queryableData.Provider));
            queryableSubstitute.Expression.Returns(queryableData.Expression);
            queryableSubstitute.ElementType.Returns(queryableData.ElementType);
            queryableSubstitute.GetEnumerator().Returns(queryableData.GetEnumerator());

            // Mock addition
            ((DbSet<TType>)queryableSubstitute).Add(null).ReturnsForAnyArgs(callInfo => SimulateAddition(callInfo, data));

            // Mock async
            ((IDbAsyncEnumerable<TType>)queryableSubstitute).GetAsyncEnumerator().Returns(new TestDbAsyncEnumerator<TType>(data.GetEnumerator()));

            return queryableSubstitute;
        }

        /// <summary>
        /// Reads <typeparamref name="TType"/> from <paramref name="callInfo"/> and stores it to the <paramref name="data"/>.
        /// To emulate reald DB, it also sets <see cref="ModelBase.Id"/> with new <see cref="Guid"/> and returns the very
        /// object the method was provided with.
        /// </summary>
        private static TType SimulateAddition<TType>(CallInfo callInfo, ICollection<TType> data)
            where TType : ModelBase
        {
            TType entry = callInfo.Arg<TType>();

            entry.Id = Guid.NewGuid();
            data.Add(callInfo.Arg<TType>());

            return entry;
        }

        protected BaseRepositoryTest()
        {
            var contextSubstitute = Substitute.For<MyWalletContext>();

            //currencies
            var c1 = GetNewCurrency("CZK");
            var c2 = GetNewCurrency("EUR");
            var currencies = new List<Currency>() { c1, c2 };
            var currenciesSub = SubstituteQueryable(currencies);
            contextSubstitute.Currencies.Returns(currenciesSub);
            CurrencyRepository = new CurrencyRepository(contextSubstitute);

            //conversion ratios
            var cr1 = GetNewConversionRatio(new DateTime(2016, 11, 7), 1.5m, c1, c2);
            var cr2 = GetNewConversionRatio(new DateTime(2016, 11, 8), 1.6m, c1, c2);
            var conversionRatios = new List<ConversionRatio>() { cr1, cr2 };
            var crSub = SubstituteQueryable(conversionRatios);
            contextSubstitute.ConversionRatios.Returns(crSub);
            ConversionRatioRepository = new ConversionRatioRepository(contextSubstitute);

            //groups
            var g1 = GetNewGroup("Group1");
            var g2 = GetNewGroup("Group2");
            var groups = new List<Group>() { g1, g2 };
            var groupsSub = SubstituteQueryable(groups);
            contextSubstitute.Groups.Returns(groupsSub);
            GroupRepository = new GroupRepository(contextSubstitute);

            //budgets
            var b1 = GetNewBudget(50m, "Something", cr1, g1);
            var b2 = GetNewBudget(150m, "Something 2", cr2, g2);
            var budgets = new List<Budget>() { b1, b2 };
            var budgetsSubstitute = SubstituteQueryable(budgets);
            contextSubstitute.Budgets.Returns(budgetsSubstitute);
            BudgetRepository = new BudgetRepository(contextSubstitute);

            //categories
            var cat1 = GetNewCategory("Cat 1", "Category 1");
            var cat2 = GetNewCategory("Cat 2", "Category 2");
            var categories = new List<Category>() { cat1, cat2 };
            var categoriesSubstitute = SubstituteQueryable(categories);
            contextSubstitute.Categories.Returns(categoriesSubstitute);
            CategoryRepository = new CategoryRepository(contextSubstitute);

            //users
            var u1 = GetNewUser("User 1", "email1@email.com");
            var u2 = GetNewUser("User 2", "email2@email.com");
            var users = new List<User>() { u1, u2 };
            var usersSub = SubstituteQueryable(users);
            contextSubstitute.Users.Returns(usersSub);
            UserRepository = new UserRepository(contextSubstitute);

            //entries
            var e1 = GetNewEntry(10m, "Entry 1", new DateTime(2016, 11, 7), u1, new List<Category>() {cat1});
            var e2 = GetNewEntry(20m, "Entry 2", new DateTime(2016, 11, 8), u2, new List<Category> { cat2});
            var entries = new List<Entry>() { e1, e2 };
            var entriesSub = SubstituteQueryable(entries);
            contextSubstitute.Entries.Returns(entriesSub);
            EntryRepository = new EntryRepository(contextSubstitute);
        }

        private Budget GetNewBudget(decimal amount, string description, ConversionRatio ratio = null, Group group = null)
        {
            return new Budget()
            {
                Id = new Guid(),
                Amount = amount,
                Description = description,
                Group = group
            };
        }

        private Category GetNewCategory(string name, string description)
        {
            return new Category()
            {
                Id = new Guid(),
                Name = name,
                Description = description
            };
        }

        private ConversionRatio GetNewConversionRatio(DateTime date, decimal ratio, Currency curFrom = null, Currency curTo = null)
        {
            return new ConversionRatio()
            {
                Id = new Guid(),
                Date = date,
                Ratio = ratio,
                CurrencyFrom = curFrom,
                CurrencyTo = curTo
            };
        }

        private Currency GetNewCurrency(string code)
        {
            return new Currency()
            {
                Id = new Guid(),
                Code = code
            };
        }

        private Entry GetNewEntry(decimal amount, string desc, DateTime date, User group = null,List<Category> categories = null)
        {
            return new Entry()
            {
                Id = new Guid(),
                Amount = amount,
                Description = desc,
                EntryTime = date,
                User = group,
                Categories = categories
            };
        }

        private Group GetNewGroup(string name)
        {
            return new Group()
            {
                Id = new Guid(),
                Name = name
            };
        }

        private User GetNewUser(string name, string email)
        {
            return new User()
            {
                Id = new Guid(),
                Name = name,
                Email = email
            };
        }
    }
}
