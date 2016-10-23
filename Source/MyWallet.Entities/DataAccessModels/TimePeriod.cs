﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimePeriod.cs" company="FI MUNI">
//   Tobias Kamenicky, Marek Halas, Robert Havlicek, Miroslav Gasparovic
// </copyright>
// <summary>
//   The time period.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MyWallet.Entities.DataAccessModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The time period.
    /// </summary>
    public class TimePeriod : ModelBase
    {
        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        [Required, DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        [Required, DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the budgets.
        /// </summary>
        public virtual ICollection<Budget> Budgets { get; set; } = new HashSet<Budget>();
    }
}
