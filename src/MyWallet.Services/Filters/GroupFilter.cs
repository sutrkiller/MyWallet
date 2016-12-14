using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallet.Services.Filters
{
    /// <summary>
    /// Filtering groups in services.
    /// </summary>
    public class GroupFilter
    {
        /// <summary>
        /// Only groups that contains this user will be selected
        /// </summary>
        public Guid? UserId { get; set; }
    }
}
