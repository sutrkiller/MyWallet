using MyWallet.Models.Entries;
using MyWallet.Models.Graphs;
using MyWallet.Models.Statistics;

namespace MyWallet.Models.Home
{
    public class DashboardModel
    {
        public CreateEntryViewModel Entry { get; set; }
        public GraphViewModel BudgetGraph { get; set; }

        public IncomeExpenseViewModel Expense { get; set; }

        public string Note { get; set; } = "";
    }
}
