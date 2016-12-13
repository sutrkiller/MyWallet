using System;
using System.Collections.Generic;
using MyWallet.Entities.Models;

namespace MyWallet.Services.DataTransferModels
{
    public class Budget : Base
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Group Group { get; set; }
        public Entities.Models.ConversionRatio ConversionRatio { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Entry> Entries { get; set; }
    }
}