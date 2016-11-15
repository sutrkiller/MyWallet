using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyWallet.Entities.Models
{
    public class Category : ModelBase
    {
        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        [Required, MaxLength(254)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description. The brief description of a purpose of the category.
        /// </summary>
        [MaxLength(256)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the budgets. Navigable property. Budgets that relate to this category.
        /// </summary>
        public virtual ICollection<Budget> Budgets { get; set; } = new HashSet<Budget>();

        /// <summary>
        /// Gets or sets the expenses. Navigable property. Entries in this category.
        /// </summary>
        public virtual ICollection<Entry> Entries { get; set; } = new HashSet<Entry>();

    }
}
