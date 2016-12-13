using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Models.Budgets
{
    public class BudgetDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }
        public string Currency { get; set; }
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        public ICollection<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();

        public string Entries { get; set; }

        public int NumberOfEntries { get; set; }

        public GroupDTO Group { get; set; }
    }
}