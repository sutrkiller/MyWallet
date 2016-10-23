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
        /// Gets or sets the currency.
        /// </summary>
        public virtual Currency Currency { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the family.
        /// </summary>
        public virtual Group Group { get; set; }
    }
}
