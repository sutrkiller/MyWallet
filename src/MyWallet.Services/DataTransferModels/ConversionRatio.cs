using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallet.Services.DataTransferModels
{
    /// <summary>
    /// Conversion ratio transfer model
    /// </summary>
    public class ConversionRatio : Base
    {
        /// <summary>
        /// Exact number that will be used to convert entities from CurrencyFrom to CurrencyTo
        /// </summary>
        public decimal Ratio { get; set; }

        /// <summary>
        /// Help tag whether this is custom or downloaded ratio
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Date when ratio added
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Source currency to convert from
        /// </summary>
        public virtual Currency CurrencyFrom { get; set; }

        /// <summary>
        /// Destination currency to convert to
        /// </summary>
        public virtual Currency CurrencyTo { get; set; }
    }
}
