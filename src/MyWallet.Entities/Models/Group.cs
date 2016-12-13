using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyWallet.Entities.Models
{
    /// <summary>
    /// Grouping users enables creating shared budgets.
    /// </summary>
    public class Group : ModelBase
    {
        /// <summary>
        /// Gets or sets the name of the group.
        /// </summary>
        [Required, MaxLength(254)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the users. Navigable property. Users belonging to this group. Might be only one user.
        /// </summary>
        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();

        /// <summary>
        /// Gets or sets the budgets. Navigable property. Budgets of this group.
        /// </summary>
        public virtual ICollection<Budget> Budgets { get; set; } = new HashSet<Budget>();
    }
}