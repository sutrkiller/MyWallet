using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MyWallet.Models.Home
{
    public class GraphViewModel
    {
        public string GraphTitle { get; set; }
        public string DateTitle { get; set; }
        public string BudgetTitle { get; set; }
        public string EntriesTitle { get; set; }

        public ICollection<string> Labels { get; set; }
        public ICollection<decimal> Entries { get; set; }
        public decimal Budget { get; set; }
        public List<decimal> Incomes { get; set; }
        public List<decimal> Expenses { get; set; }
    }
}
