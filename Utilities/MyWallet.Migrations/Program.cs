// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="FI MUNI">
//  Tobias Kamenicky, Marek Halas, Robert Havlicek, Miroslav Gasparovic  
// </copyright>
// <summary>
//   Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using MyWallet.Migrations.Migrations;

namespace MyWallet.Migrations
{
    using System;
    using System.Data.Entity;

    using MyWallet.Entities.Contexts;
    

    /// <summary>
    /// The program.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The connectio n_ strin g_ name.
        /// </summary>
        private const string CONNECTION_STRING_NAME = "MyWalletConnection";
        
        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Setting migration initializer...");

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MyWalletContext, MyWalletContextConfiguration>(useSuppliedContext:true));

            using (var db = new MyWalletContext(CONNECTION_STRING_NAME))
            {
                Console.WriteLine($"Initializing {typeof(MyWalletContext).FullName}...");
                db.Database.Initialize(force:true);
            }

            Console.WriteLine("Initialization successful...");
            Console.ReadKey();
        }
    }
}
