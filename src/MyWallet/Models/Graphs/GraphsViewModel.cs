using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWallet.Models.Home;

namespace MyWallet.Models.Graphs
{
    public class GraphsViewModel
    {
        public GraphViewModel BudgetGraphModel { get; set; }
        public GraphViewModel BudgetGraphCategoriesModel { get; set; }
    }
}
