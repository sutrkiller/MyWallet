// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MyWalletContext.cs" company="FI MUNI">
//   Tobias Kamenicky, Marek Halas, Robert Havlicek, Miroslav Gasparovic
// </copyright>
// <summary>
//   The my wallet context.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace MyWallet.Entities.Contexts
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using MyWallet.Entities.DataAccessModels;

    /// <summary>
    /// The my wallet context.
    /// </summary>
    public class MyWalletContext : DbContext
    {
        public MyWalletContext()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MyWalletContext"/> class.
        /// </summary>
        /// <param name="nameOrConnectionString">
        /// The name or connection string.
        /// </param>
        public MyWalletContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            this.Database.Log = log => this.Log?.Invoke(log);
        }

        /// <summary>
        /// Gets or sets the budgets.
        /// </summary>
        public virtual DbSet<Budget> Budgets { get; set; }

        /// <summary>
        /// Gets or sets the entries.
        /// </summary>
        public virtual DbSet<Entry> Entries { get; set; }

        /// <summary>
        /// Gets or sets the groups.
        /// </summary>
        public virtual DbSet<Group> Groups { get; set; }

        /// <summary>
        /// Gets or sets the time periods.
        /// </summary>
        public virtual DbSet<TimePeriod> TimePeriods { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        public virtual DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        public virtual DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the conversion ratios.
        /// </summary>
        public virtual DbSet<ConversionRatio> ConversionRatios { get; set; }

        /// <summary>
        /// Gets or sets the currencies.
        /// </summary>
        public virtual DbSet<Currency> Currencies { get; set; }

        /// <summary>
        /// Gets or sets the log.
        /// </summary>
        internal Action<string> Log { get; set; }

        /// <inheritdoc />
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Group>().HasMany(g => g.Users).WithMany(u => u.Groups).Map(m => m.ToTable("GroupUsers"));
            modelBuilder.Entity<Group>().HasMany(g => g.Budgets).WithOptional(b => b.Group).WillCascadeOnDelete();

            modelBuilder.Entity<Group>().HasMany(u => u.Entries).WithRequired(e => e.Group).WillCascadeOnDelete();
            //modelBuilder.Entity<User>().HasMany(u => u.Entries).WithRequired(e => e.User).WillCascadeOnDelete();
            //modelBuilder.Entity<User>().HasMany(u => u.Budgets).WithOptional(b => b.User).WillCascadeOnDelete();

            modelBuilder.Entity<Budget>().HasMany(b => b.TimePeriods).WithRequired(t => t.Budget).WillCascadeOnDelete();
            modelBuilder.Entity<Budget>()
                .HasMany(b => b.Categories)
                .WithMany(c => c.Budgets)
                .Map(m => m.ToTable("BudgetCategories"));
            modelBuilder.Entity<Budget>()
                .HasMany(b => b.Entries)
                .WithMany(e => e.Budgets)
                .Map(m => m.ToTable("BudgetEntries"));
            modelBuilder.Entity<Budget>().HasRequired(b => b.ConversionRatio).WithMany(c => c.Budgets).WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>().HasMany(c => c.Entries).WithRequired(e => e.Category).WillCascadeOnDelete(true);

            modelBuilder.Entity<Entry>()
                .HasRequired(e => e.ConversionRatio)
                .WithMany(c => c.Entries)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ConversionRatio>()
                .HasRequired(cr => cr.CurrencyFrom)
                .WithMany(c => c.ConversionRatiosFrom)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<ConversionRatio>()
                .HasRequired(cr => cr.CurrencyTo)
                .WithMany(c => c.ConversionRatiosTo)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}