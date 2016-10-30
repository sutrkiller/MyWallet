// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Expense.cs" company="FI MUNI">
//   Tobias Kamenicky, Marek Halas, Robert Havlicek, Miroslav Gasparovic
// </copyright>
// <summary>
//   Defines the Expense type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MyWallet.Entities.DataAccessModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// The expense.
    /// </summary>
    public class Entry : ModelBase
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [Required, DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [MaxLength(254)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the entry date time.
        /// </summary>
        [Required, Index, DataType(DataType.DateTime)]
        public DateTime EntryDateTime { get; set; }

        /// <summary>
        /// Gets or sets the user. Navigable property.
        /// </summary>
        //[Required]
        //public virtual User User { get; set; }

        [Required]
        public virtual Group Group { get; set; }

        /// <summary>
        /// Gets or sets the category. Navigable property.
        /// </summary>
        [Required]
        public virtual Category Category { get; set; }

        /// <summary>
        /// Gets or sets the budgets. Navigable property.
        /// </summary>
        public virtual ICollection<Budget> Budgets { get; set; } = new HashSet<Budget>();

        /// <summary>
        /// Gets or sets the ration. Navigable property.
        /// </summary>
        [Required]
        public virtual ConversionRatio ConversionRatio { get; set; }
    }
}
