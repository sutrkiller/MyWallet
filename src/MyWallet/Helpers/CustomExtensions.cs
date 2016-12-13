using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using MyWallet.Services.DataTransferModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MyWallet.Helpers
{
    public static class CustomExtensions
    {
        public static string FormatCurrency(this decimal amount, string currencyCode)
        {
            var culture = (from c in CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                           let r = new RegionInfo(c.LCID)
                           where r != null
                           && string.Equals(r.ISOCurrencySymbol, currencyCode, StringComparison.InvariantCultureIgnoreCase)
                           select c).FirstOrDefault();

            return culture == null ? amount.ToString("0.00") : string.Format(culture, "{0:C}", amount);
        }

        public static decimal ToCurrency(this EntryDTO source, decimal ratio)
        {
            if (source == null) return 0m;
            if (ratio == 0) return 0m;
            return decimal.Divide(decimal.Multiply(source.Amount, source.ConversionRatio.Ratio), ratio);
        }
    }
}
