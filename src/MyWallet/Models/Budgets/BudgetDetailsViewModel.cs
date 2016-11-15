using System;
using System.Collections.Generic;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Models.Budgets
{
    public class BudgetDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();

        public GroupDTO Group { get; set; }
    }
}