using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWallet.Entities.Models;

namespace MyWallet.Services.DataTransferModels
{
    public class Category : Base
    {
        public string Name { get; set; }

        public string Description { get; set; }
        
        public ICollection<Entry> Entries { get; set; }
    }
}
