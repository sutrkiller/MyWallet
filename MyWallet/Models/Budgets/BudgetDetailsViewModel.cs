using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallet.Models.Budgets
{
    public class BudgetDetailsViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }
    }
}
