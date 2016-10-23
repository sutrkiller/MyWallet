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
        public decimal Value { get; set; }

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
        /// Gets or sets the currency.
        /// </summary>
        [Required]
        public virtual Currency Currency { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        [Required]
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        [Required]
        public virtual Category Category { get; set; }
    }
}
