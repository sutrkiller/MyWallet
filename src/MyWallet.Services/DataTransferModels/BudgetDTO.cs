using System;
using System.Collections.Generic;
using MyWallet.Entities.Models;

namespace MyWallet.Services.DataTransferModels
{
    public class BudgetDTO : BaseDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public GroupDTO Group { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}