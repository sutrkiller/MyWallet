using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Models.Users
{
    public class ManageUserCreateViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public Guid OriginalCurrencyId { get; set; }
        [DisplayName("Preferred currency")]
        public Guid CurrencyId { get; set; }
        public SelectList CurrenciesList { get; set; }

        public ICollection<GroupDTO> Groups { get; set; }

        
    }
}
