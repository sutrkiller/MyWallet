using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MyWallet.Entities.Models;

namespace MyWallet.Entities.Contexts
{
    public class MyWalletContext : DbContext
    {
        /// <summary>
        /// Empty constructor required by migrations.
        /// </summary>
        public MyWalletContext() : base()
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

        public virtual DbSet<Budget> Budgets { get; set; }
        public virtual DbSet<Entry> Entries { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ConversionRatio> ConversionRatios { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }

        /// <summary>
        /// Exposes logging capabilities.
        /// </summary>
        internal Action<string> Log { get; set; }


        /// <inheritdoc />
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>().HasMany(g => g.Budgets).WithRequired(g => g.Group).WillCascadeOnDelete();
            modelBuilder.Entity<Group>().HasMany(g => g.Users).WithMany(u => u.Groups).Map(m => m.ToTable("UserGroups"));

            modelBuilder.Entity<User>().HasMany(u=>u.Entries).WithRequired(e=>e.User).WillCascadeOnDelete();

            modelBuilder.Entity<Entry>().HasRequired(e=>e.Category).WithMany(c=>c.Entries).WillCascadeOnDelete();
            modelBuilder.Entity<Entry>().HasRequired(e=>e.ConversionRatio).WithMany(cr=>cr.Entries).WillCascadeOnDelete(false);
            modelBuilder.Entity<Entry>()
                .HasMany(e => e.Budgets)
                .WithMany(b => b.Entries)
                .Map(m => m.ToTable("BudgetEntries"));

            modelBuilder.Entity<Budget>()
                .HasMany(b => b.Categories)
                .WithMany(c => c.Budgets)
                .Map(m => m.ToTable("BudgetCategories"));
            
            modelBuilder.Entity<ConversionRatio>().HasRequired(cr=>cr.CurrencyFrom).WithMany(cc=>cc.ConversionRatiosFrom).WillCascadeOnDelete(false);
            modelBuilder.Entity<ConversionRatio>().HasRequired(cr=>cr.CurrencyTo).WithMany(cc=>cc.ConversionRatiosTo).WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
