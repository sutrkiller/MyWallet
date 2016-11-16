using System;
using System.Collections.Generic;
using MyWallet.Entities.Models;

namespace MyWallet.Services.DataTransferModels
{
    public class EntryDTO : BaseDTO
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime EntryTime { get; set; }
        public UserDTO User { get; set; }
        public ICollection<CategoryDTO> Categories { get; set; }
        public ConversionRatio ConversionRatio { get; set; }
        public ICollection<BudgetDTO> Budgets { get; set; } = new HashSet<BudgetDTO>();
    }
}