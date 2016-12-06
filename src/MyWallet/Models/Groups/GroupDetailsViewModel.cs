using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MyWallet.Entities.Models;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Models.Groups
{
    public class GroupDetailsViewModel
    {
        public string Name { get; set; }
        [Display(Name = "Users")]
        public string UserNames { get; set; }
        public ICollection<BudgetDTO> Budgets { get; set; } = new HashSet<BudgetDTO>();
    }
}