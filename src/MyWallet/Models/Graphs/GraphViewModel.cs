// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyWallet.Models.Graphs
{
    public class GraphViewModel
    {
        public string GraphTitle { get; set; }
        public List<string> ColumnTitles { get; set; } = new List<string>();

        public Guid BudgetId { get; set; }
        public SelectList Budgets { get; set; }
    }
}
