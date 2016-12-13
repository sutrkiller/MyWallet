using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyWallet.Entities.Models
{

    /// <summary>
    /// Users serves for authorization. 
    /// </summary>
    public class User : ModelBase
    {
        /// <summary>
        /// Gets or sets the name of the user. First and last names together.
        /// </summary>
        [MaxLength(256)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email of the user. This is important for authorization.
        /// </summary>
        [Required, MaxLength(254), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the groups. Navigable property. Groups this user belongs to.
        /// </summary>
        public virtual ICollection<Group> Groups { get; set; } = new HashSet<Group>();
        /// <summary>
        /// Gets or sets the entries. Navigable property. All entries of this user.
        /// </summary>
        public virtual ICollection<Entry> Entries { get; set; } = new HashSet<Entry>();


        /// <summary>
        /// All data will be converted into this currency so the user has better overview of spent money.
        /// </summary>
        [Required]
        public virtual Currency PreferredCurrency { get; set; }

    }
}