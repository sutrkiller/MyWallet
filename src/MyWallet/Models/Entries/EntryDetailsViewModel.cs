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
        [DisplayName("Amount converted")]
        public string AmountInMain { get; set; }
        public string Description { get; set; }
        public DateTime EntryTime { get; set; }
        [Display(Name = "User")]
        public string UserName { get; set; }
        [Display(Name = "Categories")]
        public string CategoryNames { get; set; }
        public ICollection<BudgetDTO> Budgets { get; set; } = new HashSet<BudgetDTO>();
    }
}