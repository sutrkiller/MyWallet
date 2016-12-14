using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallet.Services.Filters
{
    /// <summary>
    /// Filtering entries from service
    /// </summary>
    [Serializable]
    public class EntriesFilter
    {
        /// <summary>
        /// Returned entries must be after or at this date.
        /// </summary>
        public DateTime? From { get; set; }
        /// <summary>
        /// Return entries must be before or at this date
        /// </summary>
        public DateTime? To { get; set; }
        /// <summary>
        /// Author of entries
        /// </summary>
        public Guid? UserId { get; set; }
        /// <summary>
        /// Entries must belong to budget with this id
        /// </summary>
        public Guid? BudgetId { get; set; }
        /// <summary>
        /// Entries must belong to category with this id
        /// </summary>
        public Guid? CategoryId { get; set; }
    }
}
