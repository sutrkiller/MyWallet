using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Models.Entries
{
    public class EntryViewModel
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        [Display(Name = "Entry Time")]
        public DateTime EntryTime { get; set; }
        public UserDTO User { get; set; }
        public ICollection<CategoryDTO> Categories { get; set; }
    }
}
