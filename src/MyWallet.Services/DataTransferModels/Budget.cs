using System;
using System.Collections.Generic;
using MyWallet.Entities.Models;

namespace MyWallet.Services.DataTransferModels
{
    /// <summary>
    /// Budget transfer model
    /// </summary>
    public class Budget : Base
    {
        /// <summary>
        /// Name of budget
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of budget
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Budget amount
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Budget start date
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// Budget end date
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// Budget group
        /// </summary>
        public Group Group { get; set; }
        /// <summary>
        /// Conversion ratio that will be used to convert entries to this budget
        /// </summary>
        public Entities.Models.ConversionRatio ConversionRatio { get; set; }
        /// <summary>
        /// Categories of this budget
        /// </summary>
        public ICollection<Category> Categories { get; set; }
        /// <summary>
        /// Entries belonging to the budget
        /// </summary>
        public ICollection<Entry> Entries { get; set; }
    }
}