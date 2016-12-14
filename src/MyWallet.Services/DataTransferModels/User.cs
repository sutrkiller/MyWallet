using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallet.Services.DataTransferModels
{
    /// <summary>
    /// User transfer model
    /// </summary>
    public class User : Base
    {
        /// <summary>
        /// Name of user. Both first and last name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Email used for authentication
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Currency used for displaying global statistics and to preselect currency when adding entries.
        /// </summary>
        public Currency PreferredCurrency { get; set; }
        /// <summary>
        /// Groups this user belongs to
        /// </summary>
        public ICollection<Group> Groups { get; set; }
        /// <summary>
        /// Entries of the user
        /// </summary>
        public ICollection<Entry> Entries { get; set; }
    }
}
