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
    using System.ComponentModel.DataAnnotations;

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
        public virtual Currency Currency { get; set; }
    }
}
