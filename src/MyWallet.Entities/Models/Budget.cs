using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MyWallet.Entities.Models
{
    public class Budget : ModelBase
    {
        /// <summary>
        /// Gets or sets the value of budget. Must be positive.
        /// </summary>
        [Required, DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        [Required, DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        [Required, DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the description. Optional details about the budget.
        /// </summary>
        [MaxLength(254)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name. Brief subject of budget.
        /// </summary>
        [MaxLength(254)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the categories. Navigable property. Budget can include multiple categories.
        /// </summary>
        public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();

        /// <summary>
        /// Gets or sets the entries. Navigable property. Entries that belong to this budget.
        /// </summary>
        public virtual ICollection<Entry> Entries { get; set; } = new HashSet<Entry>();

        /// <summary>
        /// Gets or sets the group. Navigable property. Group of one or more users, whose entries this budget controls.
        /// </summary>
        public virtual Group Group { get; set; }
    }
}