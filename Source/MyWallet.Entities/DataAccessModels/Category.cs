// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Category.cs" company="FI MUNI">
//   Tobias Kamenicky, Marek Halas, Robert Havlicek, Miroslav Gasparovic
// </copyright>
// <summary>
//   Defines the Category type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MyWallet.Entities.DataAccessModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The category.
    /// </summary>
    public class Category : ModelBase
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required, MaxLength(254)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [MaxLength(256)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the budgets.
        /// </summary>
        public virtual ICollection<Budget> Budgets { get; set; } = new HashSet<Budget>();

        /// <summary>
        /// Gets or sets the expenses.
        /// </summary>
        public virtual ICollection<Entry> Expenses { get; set; } = new HashSet<Entry>();

    }
}
