using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallet.Services.DataTransferModels
{
    public class ConversionRatio : Base
    {
        public decimal Ratio { get; set; }

        public string Type { get; set; }

        public DateTime Date { get; set; }

        public virtual Currency CurrencyFrom { get; set; }

        public virtual Currency CurrencyTo { get; set; }
    }
}
