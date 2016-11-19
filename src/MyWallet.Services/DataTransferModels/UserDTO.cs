using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallet.Services.DataTransferModels
{
    public class UserDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public CurrencyDTO PreferredCurrency { get; set; }
        public ICollection<GroupDTO> Groups { get; set; }
        public ICollection<EntryDTO> Entries { get; set; }
    }
}
