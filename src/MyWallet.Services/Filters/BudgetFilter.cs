using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallet.Services.Filters
{
    /// <summary>
    /// Filtering budgets in services
    /// </summary>
    public class BudgetFilter
    {
        /// <summary>
        /// Only budgets with groups containing user with this id will be selected
        /// </summary>
        public Guid? UserId { get; set; }
    }
}
