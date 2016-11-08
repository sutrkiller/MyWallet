using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using MyWallet.Entities.Contexts;
using MyWallet.Entities.DataAccessModels;
using MyWallet.Entities.Repositories;
using NSubstitute;
using NSubstitute.Core;

namespace MyWallet.Entities.UnitTests.Repositories
{
    public abstract class BaseRepositoryTest
    {
        protected readonly BudgetRepository BudgetRepository;
        protected readonly CategoryRepository CategoryRepository;
        protected readonly ConversionRatioRepository ConversionRatioRepository;
        protected readonly CurrencyRepository CurrencyRepository;
        protected readonly EntryRepository EntryRepository;
        protected readonly GroupRepository GroupRepository;
        protected readonly TimePeriodRepository TimePeriodRepository;
        protected readonly UserRepository UserRepository;


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
            var c1 = GetNewCurrency("CZK");
            var c2 = GetNewCurrency("EUR");

            var cr1 = GetNewConversionRatio(new DateTime(2016, 11, 7), 1.5m,c1,c2);
            var cr2 = GetNewConversionRatio(new DateTime(2016, 11, 8), 1.6m,c1,c2);

            var g1 = GetNewGroup("Group1");
            var g2 = GetNewGroup("Group2");

            var b1 = GetNewBudget(50m, "Something",cr1,g1);
            var b2 = GetNewBudget(150m, "Something 2",cr2,g2);

            var cat1 = GetNewCategory("Cat 1", "Category 1");
            var cat2 = GetNewCategory("Cat 2", "Category 2");

            var e1 = GetNewEntry(10m, "Entry 1", new DateTime(2016, 11, 7), g1, cat1);
            var e2 = GetNewEntry(20m, "Entry 2", new DateTime(2016, 11, 8), g2, cat2);

            var t1 = GetNewTimePeriod(new DateTime(2016, 11, 7), new DateTime(2016, 11, 8), b1);
            var t2 = GetNewTimePeriod(new DateTime(2016, 11, 8), new DateTime(2016, 11, 9), b2);

            var u1 = GetNewUser("User 1", "email1@email.com");
            var u2 = GetNewUser("User 2", "email2@email.com");

            var budgets = new List<Budget>() {b1,b2};
            var categories = new List<Category>() {cat1, cat2};
            var currencies = new List<Currency>() {c1,c2};
            var conversionRatios = new List<ConversionRatio>(){cr1,cr2};
            var entries = new List<Entry>() {e1,e2};
            var groups = new List<Group>() {g1,g2};
            var times = new List<TimePeriod>() {t1,t2};
            var users = new List<User>() {u1,u2};
            
            //Mock context
            var budgetsSubstitute = SubstituteQueryable(budgets);
            var categoriesSubstitute = SubstituteQueryable(categories);
            var currenciesSub = SubstituteQueryable(currencies);
            var crSub = SubstituteQueryable(conversionRatios);
            var entriesSub = SubstituteQueryable(entries);
            var groupsSub = SubstituteQueryable(groups);
            var timesSub = SubstituteQueryable(times);
            var usersSub = SubstituteQueryable(users);

            var contextSubstitute = Substitute.For<MyWalletContext>();
            contextSubstitute.Budgets.Returns(budgetsSubstitute);
            contextSubstitute.Categories.Returns(categoriesSubstitute);
            contextSubstitute.Currencies.Returns(currenciesSub);
            contextSubstitute.ConversionRatios.Returns(crSub);
            contextSubstitute.Entries.Returns(entriesSub);
            contextSubstitute.Groups.Returns(groupsSub);
            contextSubstitute.TimePeriods.Returns(timesSub);
            contextSubstitute.Users.Returns(usersSub);
            BudgetRepository = new BudgetRepository(contextSubstitute);
            CategoryRepository = new CategoryRepository(contextSubstitute);
            ConversionRatioRepository = new ConversionRatioRepository(contextSubstitute);
            CurrencyRepository = new CurrencyRepository(contextSubstitute);
            EntryRepository = new EntryRepository(contextSubstitute);
            GroupRepository = new GroupRepository(contextSubstitute);
            TimePeriodRepository = new TimePeriodRepository(contextSubstitute);
            UserRepository = new UserRepository(contextSubstitute);
        }

        private Budget GetNewBudget(decimal amount, string description, ConversionRatio ratio = null, Group group = null)
        {
            return new Budget()
            {
                Id = new Guid(),
                Amount = amount,
                Description = description,
                ConversionRatio = ratio,
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

        private Entry GetNewEntry(decimal amount, string desc, DateTime date, Group group = null, Category category = null)
        {
            return new Entry()
            {
                Id = new Guid(),
                Amount = amount,
                Description = desc,
                EntryDateTime = date,
                Group = group,
                Category = category
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

        private TimePeriod GetNewTimePeriod(DateTime start, DateTime end, Budget budget = null)
        {
            return new TimePeriod()
            {
                Id = new Guid(),
                Budget = budget,
                StartDate = start,
                EndDate = end
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
