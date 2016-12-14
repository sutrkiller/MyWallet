using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWallet.Entities.Models
{

    /// <summary>
    /// Common parent class for all models adding Id for db access.
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
