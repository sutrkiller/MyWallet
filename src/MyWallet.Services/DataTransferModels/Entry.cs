using System;
using System.Collections.Generic;
using MyWallet.Entities.Models;

namespace MyWallet.Services.DataTransferModels
{
    /// <summary>
    /// Entry is the most important entity. It is used to show each expense or income.
    /// </summary>
    public class Entry : Base
    {
        /// <summary>
        /// Value of entry. Positive for Incomes, negative for outcomes.
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Brief description of entry
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Time (date + hourse, minutes) 
        /// </summary>
        public DateTime EntryTime { get; set; }

        /// <summary>
        /// Entry added by user
        /// </summary>
        public User User { get; set; }
        
        /// <summary>
        /// Entry belongs to categories
        /// </summary>
        public ICollection<Category> Categories { get; set; }
        /// <summary>
        /// Each entry has conversion ratio custom or from net.
        /// </summary>
        public Entities.Models.ConversionRatio ConversionRatio { get; set; }
        /// <summary>
        /// Entry may belong to many budgets
        /// </summary>
        public ICollection<Budget> Budgets { get; set; } = new HashSet<Budget>();
    }
}