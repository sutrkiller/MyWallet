using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallet.Services.DataTransferModels
{
    /// <summary>
    /// Grouping users enables creating budgets for families, etc.
    /// </summary>
    public class Group : Base
    {
        /// <summary>
        /// Name of group
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Created budgets
        /// </summary>
        public ICollection<Budget> Budgets { get; set; }
        /// <summary>
        /// Users belonging to this group
        /// </summary>
        public ICollection<User> Users { get; set; }
    }
}
