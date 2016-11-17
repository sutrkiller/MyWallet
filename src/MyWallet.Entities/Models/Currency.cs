using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWallet.Entities.Models
{
    public class Currency : ModelBase
    {
        /// <summary>
        /// Gets or sets the code. Currency code (CZK, USD, EUR,...)
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

        /// <summary>
        /// Collection of users whose preferred currency is this.
        /// </summary>
        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();

    }
}