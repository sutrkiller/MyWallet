using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWallet.Entities.Models;

namespace MyWallet.Services.DataTransferModels
{
    /// <summary>
    /// Category transfer model
    /// </summary>
    public class Category : Base
    {
        /// <summary>
        /// Name of category
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of category
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Entries belonging to the category
        /// </summary>
        public ICollection<Entry> Entries { get; set; }
    }
}
