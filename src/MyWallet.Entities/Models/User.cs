using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyWallet.Entities.Models
{
    public class User : ModelBase
    {
        /// <summary>
        /// Gets or sets the name of the user.
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

        [Required]
        public virtual Currency PreferredCurrency { get; set; }

    }
}