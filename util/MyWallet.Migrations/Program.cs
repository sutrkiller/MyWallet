using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWallet.Entities.Contexts;
using MyWallet.Migrations.Migrations;

namespace MyWallet.Migrations
{
    public class Program
    {
        /// <summary>
        /// The connection string to initialize Db.
        /// </summary>
        private const string ConnectionStringName = "MyWalletDb";

        static void Main(string[] args)
        {
            Console.WriteLine("Setting migration initializer...");

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MyWalletContext, Configuration>(useSuppliedContext: true));

            using (var db = new MyWalletContext(ConnectionStringName))
            {
                Console.WriteLine($"Initializing {typeof(MyWalletContext).FullName}...");
                db.Database.Initialize(force: true);
            }

            Console.WriteLine("Initialization successful...");
        }
    }
}
