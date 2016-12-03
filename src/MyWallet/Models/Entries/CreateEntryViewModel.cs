using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Models.Entries
{
    public class CreateEntryViewModel
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string CustomRatioAmount { get; set; }
        [Display(Name = "Custom conversion ratios")]
        public Guid CustomRatioCurrencyId { get; set; }
        public SelectList CustomCurrenciesList { get; set; }
        public string Description { get; set; }
        public bool IsIncome { get; set; } = true;
        [Display(Name = "Entry time")]
        public DateTime EntryTime { get; set; }
        [Display(Name = "User")]
        public Guid UserId { get; set; }
        [Display(Name = "Categories")]
        public ICollection<Guid> CategoryIds { get; set; } = new List<Guid>();
        public SelectList CategoriesList { get; set; }
        [Display(Name = "Budgets")]
        public ICollection<Guid> BudgetIds { get; set; } = new List<Guid>();
        public SelectList BudgetsList { get; set; }
        public Guid CurrencyId { get; set; }
        public SelectList CurrenciesList { get; set; }
        [Display(Name = "Conversion ratio")]
        public Guid ConversionRatioId { get; set; }
        public SelectList ConversionRatiosList { get; set; }
    }
}
