using System;
using System.Collections.Generic;
using System.ComponentModel;
using MyWallet.Entities.Models;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Models.Groups
{
    public class GroupDetailsViewModel
    {
        [DisplayName("Group name")]
        public string Name { get; set; }
        [DisplayName("Users")]
        public string UserNames { get; set; }
        public ICollection<BudgetDTO> Budgets { get; set; } = new HashSet<BudgetDTO>();
    }
}