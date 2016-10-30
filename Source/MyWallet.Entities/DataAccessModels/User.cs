// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.cs" company="FI MUNI">
//   Tobias Kamenicky, Marek Halas, Robert Havlicek, Miroslav Gasparovic
// </copyright>
// <summary>
//   The user model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MyWallet.Entities.DataAccessModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The user model.
    /// </summary>
    public class User : ModelBase
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [MaxLength(256)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Required, MaxLength(254), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the groups. Navigable property.
        /// </summary>
        public virtual ICollection<Group> Groups { get; set; } = new HashSet<Group>();

        ///// <summary>
        ///// Gets or sets the entries. Navigable property.
        ///// </summary>
        //public virtual ICollection<Entry> Entries { get; set; } = new HashSet<Entry>();

        ///// <summary>
        ///// Gets or sets the budgets. Navigable property.
        ///// </summary>
        //public virtual ICollection<Budget> Budgets { get; set; } = new HashSet<Budget>();
    }
}
