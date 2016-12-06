using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MyWallet.Entities.Models;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Models.Entries
{
    public class EntryDetailsViewModel
    {
        public string Amount { get; set; }
        [Display(Name = "Amount converted")]
        public string AmountInMain { get; set; }
        public string Description { get; set; }
        [Display(Name = "Entry time")]
        public DateTime EntryTime { get; set; }
        [Display(Name = "User")]
        public string UserName { get; set; }
        [Display(Name = "Categories")]
        public string CategoryNames { get; set; }
        [Display(Name = "Budgets")]
        public string BudgetsNames { get; set; }
    }
}