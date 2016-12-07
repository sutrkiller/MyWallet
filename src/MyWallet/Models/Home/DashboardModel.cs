using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWallet.Models.Budgets;
using MyWallet.Models.Entries;

namespace MyWallet.Models.Home
{
    public class DashboardModel
    {
        public CreateEntryViewModel Entry { get; set; }
        public GraphViewModel BudgetGraph { get; set; }
        public string Note { get; set; } = "";
    }
}
