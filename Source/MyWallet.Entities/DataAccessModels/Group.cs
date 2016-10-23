// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Group.cs" company="FI MUNI">
//  Tobias Kamenicky, Marek Halas, Robert Havlicek, Miroslav Gasparovic   
// </copyright>
// <summary>
//   The family.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MyWallet.Entities.DataAccessModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The family.
    /// </summary>
    public class Group : ModelBase
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required, MaxLength(254)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();

        /// <summary>
        /// Gets or sets the budgets.
        /// </summary>
        public virtual ICollection<Budget> Budgets { get; set; } = new HashSet<Budget>();
    }
}
