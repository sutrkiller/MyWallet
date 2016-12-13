using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyWallet.Entities.Models
{
    /// <summary>
    /// Budget is specified for a group. Users can specify budget for their groups so they can keep an eye on entries added in time period specified by budget.
    /// </summary>
    public class Budget : ModelBase
    {
        /// <summary>
        /// Gets or sets the value of budget. Must be positive. Just a control amount without any restrictions on further spending.
        /// </summary>
        [Required, DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the date when entries will start being included in the budget.
        /// </summary>
        [Required, DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the date when entries will stop being included in the budget.
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
        /// Currency of created budget. It is possible to convert entries values into this currency.
        /// </summary>
        [Required]
        public virtual ConversionRatio ConversionRatio { get; set; }

        /// <summary>
        /// Gets or sets the group. Navigable property. Group of one or more users, whose entries this budget controls.
        /// </summary>
        public virtual Group Group { get; set; }
    }
}