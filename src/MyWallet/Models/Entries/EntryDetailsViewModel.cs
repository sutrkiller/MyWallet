using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("User")]
        public string UserName { get; set; }
        [DisplayName("Categories")]
        public ICollection<CategoryDTO> Categories { get; set; }
        public ICollection<BudgetDTO> Budgets { get; set; } = new HashSet<BudgetDTO>();
    }
}