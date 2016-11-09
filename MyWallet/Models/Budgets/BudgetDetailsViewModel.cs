using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Models.Budgets
{
    public class BudgetDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public Currency Currency { get; set; }

        public ICollection<TimePeriod> TimePeriods { get; set; }

        public ICollection<Category> Categories { get; set; }

        public Group Group { get; set; }
    }
}
