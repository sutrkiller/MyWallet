using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyWallet.Entities.Models
{
    public class ConversionRatio : ModelBase
    {
        /// <summary>
        /// Gets or sets the ratio. Ratio that converts CurrencyFrom to CurrencyTo.
        /// </summary>
        [Required]
        public decimal Ratio { get; set; }

        /// <summary>
        /// Gets or sets the date. Date when this ratio is valid.
        /// </summary>
        [Required, DataType(DataType.Date)]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the currency. This is navigable property. Currency this ratio can convert from.
        /// </summary>
        public virtual Currency CurrencyFrom { get; set; }

        /// <summary>
        /// Gets or sets the currency to. Navigable property. Currency this ratio can convert to. Always the main currency.
        /// </summary>
        public virtual Currency CurrencyTo { get; set; }

        /// <summary>
        /// Gets or sets the entries. Navigable property. Entries with this conversion ratio.
        /// </summary>
        public ICollection<Entry> Entries { get; set; } = new HashSet<Entry>();
    }
}
