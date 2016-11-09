namespace MyWallet.Migrations.Migrations
{
    using Entities.DataAccessModels;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    //Add-Migration init -StartUpProjectName MyWallet.Migrations -ProjectName MyWallet.Migrations -ConfigurationTypeName MyWalletContextConfiguration
    internal sealed class MyWalletContextConfiguration : DbMigrationsConfiguration<MyWallet.Entities.Contexts.MyWalletContext>
    {
        public MyWalletContextConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.MigrationsDirectory = @"MyWalletMigrations\Migrations";
        }

        protected override void Seed(MyWallet.Entities.Contexts.MyWalletContext context)
        {
            var b1 = new Budget { Name = "Prázdniny", Description = "Peníze které utratím na prázdninách", Amount = 222m };
            var b2 = new Budget { Name = "Chlastaèka", Description = "Co všechno propiju", Amount = 5000m };
            context.Budgets.AddOrUpdate(b1,b2);


            var c1 = new Category { Name = "Holky", Description = "Bitches! :P", Budgets = { b1, b2 } };
            var c2 = new Category { Name = "Chlast", Description = "Drinks!", Budgets = { b1, b2 } };
            var c3 = new Category { Name = "Chlebíèky", Description = "Nom nom", Budgets = { b1 } };
            context.Categories.AddOrUpdate(c1,c2,c3);

            var cur1 = new Currency { Code = "USD" };
            var cur2 = new Currency { Code = "EUR" };
            var cur3 = new Currency { Code = "CZK" };
            var cur4 = new Currency { Code = "LIB"};
            context.Currencies.AddOrUpdate(cur1, cur2, cur3, cur4);

            var con1 = new ConversionRatio { CurrencyFrom = cur1, CurrencyTo = cur2, Date = DateTime.Now, Ratio=54, Budgets = { b1 } };
            var con2 = new ConversionRatio { CurrencyFrom = cur3, CurrencyTo = cur4, Date = DateTime.Now, Ratio = 100, Budgets = { b2} };

            context.ConversionRatios.AddOrUpdate(con1, con2);

            var e1 = new Entry {Amount = 50, Category = c1,Budgets = { b1 }, Description = "Òadìžna" };
            var e5 = new Entry { Amount = 50, Category = c1, Budgets = { b1 }, Description = "Evžen-ka" };
            var e2 = new Entry { Amount = 50, Category = c2, Budgets = { b2 }, Description = "Vodka" };
            var e3 = new Entry { Amount = 50, Category = c3, Budgets = { b1 }, Description = "Bagetka po práci" };
            var e4 = new Entry { Amount = 50, Category = c2, Budgets = { b2 }, Description = "Rum" };
            context.Currencies.AddOrUpdate(cur1, cur2, cur3, cur4);




        }
    }
}
