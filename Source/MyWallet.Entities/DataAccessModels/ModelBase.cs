// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelBase.cs" company="FI MUNI">
//   Tobias Kamenicky, Marek Halas, Robert Havlicek, Miroslav Gasparovic
// </copyright>
// <summary>
//   Defines the ModelBase type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MyWallet.Entities.DataAccessModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// The model base.
    /// </summary>
    public class ModelBase
    {
        /// <summary>
        /// Gets or sets id of object. <see cref="Id"/> is marked as (primary) key and while being a <see cref="Guid"/>, also DB generated. For <c>int</c> keys, auto-increment should be used by default.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
    }
}
