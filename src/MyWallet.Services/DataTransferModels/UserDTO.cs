using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallet.Services.DataTransferModels
{
    public class User : Base
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Currency PreferredCurrency { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<Entry> Entries { get; set; }
    }
}
