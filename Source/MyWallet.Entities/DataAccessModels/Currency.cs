// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Currency.cs" company="FI MUNI">
//    Tobias Kamenicky, Marek Halas, Robert Havlicek, Miroslav Gasparovic
// </copyright>
// <summary>
//   Defines the Currency type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MyWallet.Entities.DataAccessModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// The currency.
    /// </summary>
    public class Currency : ModelBase
    {
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        [Required, Index(IsUnique = true), MaxLength(30)]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the conversion ratios from. This is navigable property.
        /// </summary>
        public virtual ICollection<ConversionRatio> ConversionRatiosFrom { get; set; } = new HashSet<ConversionRatio>();

        /// <summary>
        /// Gets or sets the conversion ratios to. This is navigable property.
        /// </summary>
        public virtual ICollection<ConversionRatio> ConversionRatiosTo { get; set; } = new HashSet<ConversionRatio>();
    }
}
