// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Budget.cs" company="FI MUNI">
//   Tobias Kamenicky, Marek Halas, Robert Havlicek, Miroslav Gasparovic
// </copyright>
// <summary>
//   The budget.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MyWallet.Entities.DataAccessModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The budget.
    /// </summary>
    public class Budget : ModelBase
    {
        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        [Required, DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [MaxLength(254)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [MaxLength(254)]
        public string Name { get; set; }

        ///// <summary>
        ///// Gets or sets the currency.
        ///// </summary>
        public virtual Currency Currency { get; set; }

        /// <summary>
        /// Gets or sets the categories. Navigable property.
        /// </summary>
        public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();

        /// <summary>
        /// Gets or sets the entries. Navigable property.
        /// </summary>
        public virtual ICollection<Entry> Entries { get; set; } = new HashSet<Entry>();

        /// <summary>
        /// Gets or sets the time periods. Navigable property.
        /// </summary>
        public virtual ICollection<TimePeriod> TimePeriods { get; set; } = new HashSet<TimePeriod>();



        /// <summary>
        /// Gets or sets the conversion ratio. Navigable property.
        /// </summary>
        [Required]
        public virtual ConversionRatio ConversionRatio { get; set; }

        /// <summary>
        /// Gets or sets the user. Navigable property.
        /// </summary>
        //public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the family. Navigable property.
        /// </summary>
        public virtual Group Group { get; set; }
    }
}
