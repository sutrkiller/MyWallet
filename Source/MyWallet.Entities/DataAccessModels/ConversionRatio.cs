// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConversionRatio.cs" company="FI MUNI">
// Tobias Kamenicky, Marek Halas, Robert Havlicek, Miroslav Gasparovic   
// </copyright>
// <summary>
//   Defines the ConversionRatio type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MyWallet.Entities.DataAccessModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Policy;

    /// <summary>
    /// The conversion ratio.
    /// </summary>
    public class ConversionRatio : ModelBase
    {
        /// <summary>
        /// Gets or sets the ratio.
        /// </summary>
        [Required]
        public decimal Ratio { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        [Required, DataType(DataType.Date)]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the currency. This is navigable property.
        /// </summary>
        public virtual Currency CurrencyFrom { get; set; }

        /// <summary>
        /// Gets or sets the currency to. Navigable property.
        /// </summary>
        public virtual Currency CurrencyTo { get; set; }

        /// <summary>
        /// Gets or sets the budgets. Navigable property.
        /// </summary>
        public ICollection<Budget> Budgets { get; set; } = new HashSet<Budget>();

        /// <summary>
        /// Gets or sets the entries. Navigable property.
        /// </summary>
        public ICollection<Entry> Entries { get; set; } = new HashSet<Entry>();
    }
}
