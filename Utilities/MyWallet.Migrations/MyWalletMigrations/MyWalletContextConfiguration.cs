// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MyWalletContextConfiguration.cs" company="FI MUNI">
//   Tobias Kamenicky, Marek Halas, Robert Havlicek, Miroslav Gasparovic
// </copyright>
// <summary>
//   The my wallet context configuration.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MyWallet.Migrations.MyWalletMigrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// The my wallet context configuration.
    /// </summary>
    internal sealed class MyWalletContextConfiguration : DbMigrationsConfiguration<MyWallet.Entities.Contexts.MyWalletContext>
    {
        public MyWalletContextConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.MigrationsDirectory = @"MyWalletMigrations\Migrations";
        }

        /// <summary>
        /// This method will be called after migrating to the latest version.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        protected override void Seed(Entities.Contexts.MyWalletContext context)
        {
        }
    }
}
