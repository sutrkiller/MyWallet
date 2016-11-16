using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWallet.Entities.Models
{
    public class Entry : ModelBase
    {
        /// <summary>
        /// Gets or sets the value. Positive value means income while negative is outcome.
        /// </summary>
        [Required, DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the description. Optional brief description of this entry.
        /// </summary>
        [MaxLength(254)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the entry time. Date and time of addition of this entry.
        /// </summary>
        [Required, Index, DataType(DataType.DateTime)]
        public DateTime EntryTime { get; set; }

        /// <summary>
        /// Gets or sets the user. Navigable property. User that had this income/outcome.
        /// </summary>
        [Required]
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the category. Navigable property. Each entry muset belong to some category.
        /// </summary>
        [Required]
        public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();

        /// <summary>
        /// Gets or sets the budgets. Navigable property. Budgets that include this entry.
        /// </summary>
        public virtual ICollection<Budget> Budgets { get; set; } = new HashSet<Budget>();

        /// <summary>
        /// Gets or sets the conversion ratio. Navigable property. Each entry can be added in different currency. They are converted to main currency.
        /// </summary>
        [Required]
        public virtual ConversionRatio ConversionRatio { get; set; }
    }
}
