using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallet.Services.Filters
{
    [Serializable]
    public class EntriesFilter
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
