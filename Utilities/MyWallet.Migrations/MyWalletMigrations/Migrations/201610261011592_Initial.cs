namespace MyWallet.Migrations.MyWalletMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Budgets",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(maxLength: 254),
                        User_Id = c.Guid(),
                        Group_Id = c.Guid(),
                        ConversionRatio_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.Group_Id, cascadeDelete: true)
                .ForeignKey("dbo.ConversionRatios", t => t.ConversionRatio_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Group_Id)
                .Index(t => t.ConversionRatio_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 254),
                        Description = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Entries",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(maxLength: 254),
                        EntryDateTime = c.DateTime(nullable: false),
                        ConversionRatio_Id = c.Guid(nullable: false),
                        User_Id = c.Guid(nullable: false),
                        Category_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ConversionRatios", t => t.ConversionRatio_Id)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.EntryDateTime)
                .Index(t => t.ConversionRatio_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.ConversionRatios",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Ratio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        CurrencyFrom_Id = c.Guid(nullable: false),
                        CurrencyTo_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Currencies", t => t.CurrencyFrom_Id)
                .ForeignKey("dbo.Currencies", t => t.CurrencyTo_Id)
                .Index(t => t.CurrencyFrom_Id)
                .Index(t => t.CurrencyTo_Id);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Code, unique: true);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(maxLength: 256),
                        Email = c.String(nullable: false, maxLength: 254),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 254),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TimePeriods",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Budget_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Budgets", t => t.Budget_Id, cascadeDelete: true)
                .Index(t => t.Budget_Id);
            
            CreateTable(
                "dbo.GroupUsers",
                c => new
                    {
                        Group_Id = c.Guid(nullable: false),
                        User_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Group_Id, t.User_Id })
                .ForeignKey("dbo.Groups", t => t.Group_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Group_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.BudgetCategories",
                c => new
                    {
                        Budget_Id = c.Guid(nullable: false),
                        Category_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Budget_Id, t.Category_Id })
                .ForeignKey("dbo.Budgets", t => t.Budget_Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .Index(t => t.Budget_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.BudgetEntries",
                c => new
                    {
                        Budget_Id = c.Guid(nullable: false),
                        Entry_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Budget_Id, t.Entry_Id })
                .ForeignKey("dbo.Budgets", t => t.Budget_Id)
                .ForeignKey("dbo.Entries", t => t.Entry_Id)
                .Index(t => t.Budget_Id)
                .Index(t => t.Entry_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimePeriods", "Budget_Id", "dbo.Budgets");
            DropForeignKey("dbo.BudgetEntries", "Entry_Id", "dbo.Entries");
            DropForeignKey("dbo.BudgetEntries", "Budget_Id", "dbo.Budgets");
            DropForeignKey("dbo.Budgets", "ConversionRatio_Id", "dbo.ConversionRatios");
            DropForeignKey("dbo.BudgetCategories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.BudgetCategories", "Budget_Id", "dbo.Budgets");
            DropForeignKey("dbo.Entries", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.GroupUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.GroupUsers", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.Budgets", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.Entries", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Budgets", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Entries", "ConversionRatio_Id", "dbo.ConversionRatios");
            DropForeignKey("dbo.ConversionRatios", "CurrencyTo_Id", "dbo.Currencies");
            DropForeignKey("dbo.ConversionRatios", "CurrencyFrom_Id", "dbo.Currencies");
            DropIndex("dbo.BudgetEntries", new[] { "Entry_Id" });
            DropIndex("dbo.BudgetEntries", new[] { "Budget_Id" });
            DropIndex("dbo.BudgetCategories", new[] { "Category_Id" });
            DropIndex("dbo.BudgetCategories", new[] { "Budget_Id" });
            DropIndex("dbo.GroupUsers", new[] { "User_Id" });
            DropIndex("dbo.GroupUsers", new[] { "Group_Id" });
            DropIndex("dbo.TimePeriods", new[] { "Budget_Id" });
            DropIndex("dbo.Currencies", new[] { "Code" });
            DropIndex("dbo.ConversionRatios", new[] { "CurrencyTo_Id" });
            DropIndex("dbo.ConversionRatios", new[] { "CurrencyFrom_Id" });
            DropIndex("dbo.Entries", new[] { "Category_Id" });
            DropIndex("dbo.Entries", new[] { "User_Id" });
            DropIndex("dbo.Entries", new[] { "ConversionRatio_Id" });
            DropIndex("dbo.Entries", new[] { "EntryDateTime" });
            DropIndex("dbo.Budgets", new[] { "ConversionRatio_Id" });
            DropIndex("dbo.Budgets", new[] { "Group_Id" });
            DropIndex("dbo.Budgets", new[] { "User_Id" });
            DropTable("dbo.BudgetEntries");
            DropTable("dbo.BudgetCategories");
            DropTable("dbo.GroupUsers");
            DropTable("dbo.TimePeriods");
            DropTable("dbo.Groups");
            DropTable("dbo.Users");
            DropTable("dbo.Currencies");
            DropTable("dbo.ConversionRatios");
            DropTable("dbo.Entries");
            DropTable("dbo.Categories");
            DropTable("dbo.Budgets");
        }
    }
}
