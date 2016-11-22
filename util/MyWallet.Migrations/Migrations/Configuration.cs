using MyWallet.Entities.Models;

namespace MyWallet.Migrations.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MyWallet.Entities.Contexts.MyWalletContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            MigrationsDirectory = @"Migrations\All";
        }

        protected override void Seed(MyWallet.Entities.Contexts.MyWalletContext context)
        {
            var g1 = new Group() { Name = "G1" };
            var g2 = new Group() { Name = "G2" };
            context.Groups.AddOrUpdate(g1, g2);

            var cur1 = new Currency() { Code = "USD" };
            var cur2 = new Currency() { Code = "EUR" };
            var cur3 = new Currency() { Code = "CZK" };
            context.Currencies.AddOrUpdate(cur1, cur2, cur3);

            var u1 = new User() { Email = "email@provider.com", Name = "User 1", PreferredCurrency = cur3 };
            context.Users.AddOrUpdate(u1);

            var con1 = new ConversionRatio() { CurrencyFrom = cur1, CurrencyTo = cur3, Date = DateTime.Now, Ratio = 1.1m };
            var con2 = new ConversionRatio() { CurrencyFrom = cur2, CurrencyTo = cur3, Date = DateTime.Now, Ratio = 1.2m };
            var con3 = new ConversionRatio() { CurrencyFrom = cur3, CurrencyTo = cur3, Date = DateTime.Now, Ratio = 1m };
            context.ConversionRatios.AddOrUpdate(con1, con2, con3);

            var b1 = new Budget { Name = "Prázdniny", Description = "Peníze které utratím na prázdninách", Amount = 222m, Group = g1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(5), ConversionRatio = con1 };
            var b2 = new Budget { Name = "Chlastaèka", Description = "Co všechno propiju", Amount = 5000m, Group = g2, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(15), ConversionRatio = con2 };
            context.Budgets.AddOrUpdate(b1, b2);


            var c1 = new Category { Name = "Holky", Description = "Bitches! :P", Budgets = { b1, b2 } };
            var c2 = new Category { Name = "Chlast", Description = "Drinks!", Budgets = { b1, b2 } };
            var c3 = new Category { Name = "Chlebíèky", Description = "Nom nom", Budgets = { b1 } };
            context.Categories.AddOrUpdate(c1, c2, c3);



            var e1 = new Entry
            {
                Amount = 50m,
                Categories = { c1 },
                Budgets = { b1 },
                Description = "Blabla",
                User = u1,
                ConversionRatio = con1,
                EntryTime = DateTime.Now
            };
            var e5 = new Entry { Amount = 50m, Categories = { c1, c2 }, Budgets = { b1 }, Description = "Evžen-ka", User = u1, ConversionRatio = con2, EntryTime = DateTime.Now };
            var e2 = new Entry { Amount = 50m, Categories = { c2 }, Budgets = { b2 }, Description = "Vodka", User = u1, ConversionRatio = con1, EntryTime = DateTime.Now };
            var e3 = new Entry { Amount = 50m, Categories = { c2, c3 }, Budgets = { b1 }, Description = "Bagetka po práci", User = u1, ConversionRatio = con1, EntryTime = DateTime.Now };
            var e4 = new Entry { Amount = 50m, Categories = { c1, c3 }, Budgets = { b2 }, Description = "Rum", User = u1, ConversionRatio = con2, EntryTime = DateTime.Now.AddDays(1) };
            context.Entries.AddOrUpdate(e1, e2, e3, e4, e5);

        }
    }
}
