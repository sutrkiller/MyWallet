using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWallet.Entities.Models;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Models.Budgets
{
    public class CreateBudgetViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<Guid> CategoryIds { get; set; } = new List<Guid>();

        public SelectList CategoriesList { get; set; }

        public Guid GroupId { get; set; }
        public SelectList GroupsList { get; set; }

        public Guid CurrencyId { get; set; }
        public SelectList CurrenciesList { get; set; }
    }
}
