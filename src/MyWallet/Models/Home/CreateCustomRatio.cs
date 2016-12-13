using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyWallet.Models.Home
{
    public class CreateCustomRatio
    {

        public decimal Amount { get; set; }
        [Display(Name = "Currency from")]
        public Guid CurrencyFromId { get; set; }
        public SelectList CurrenciesFromList { get; set; }
        [Display(Name = "Currency to")]
        public Guid CurrencyToId { get; set; }
        public SelectList CurrenciesToList { get; set; }

    }
}
