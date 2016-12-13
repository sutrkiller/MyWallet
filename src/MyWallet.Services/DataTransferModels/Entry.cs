using System;
using System.Collections.Generic;
using MyWallet.Entities.Models;

namespace MyWallet.Services.DataTransferModels
{
    public class Entry : Base
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime EntryTime { get; set; }
        public User User { get; set; }
        public ICollection<Category> Categories { get; set; }
        public Entities.Models.ConversionRatio ConversionRatio { get; set; }
        public ICollection<Budget> Budgets { get; set; } = new HashSet<Budget>();
    }
}