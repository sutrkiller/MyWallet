using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallet.Services.DataTransferModels
{
    public class ConversionRatioDTO : BaseDTO
    {
       
        public decimal Ratio { get; set; }

        public string Type { get; set; }

        public DateTime Date { get; set; }

        public virtual CurrencyDTO CurrencyFrom { get; set; }

    
        public virtual CurrencyDTO CurrencyTo { get; set; }

       
        
    }
}
