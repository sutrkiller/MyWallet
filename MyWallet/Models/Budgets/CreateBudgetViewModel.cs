using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Models.Budgets
{
    public class CreateBudgetViewModel
    {
        public Guid  Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public Guid CurrencyId { get; set; }
        public SelectList CurrencyList { get; set; }

        public ICollection<TimePeriod> TimePeriods { get; set; }

        public ICollection<Guid> CategoryIds { get; set; }

        public SelectList CategoriesList { get; set; }

        public Group Group { get; set; }


    }
}
