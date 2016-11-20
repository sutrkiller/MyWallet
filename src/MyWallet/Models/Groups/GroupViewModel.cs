using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Models.Groups
{
    public class GroupViewModel
    {
        public Guid Id { get; set; }
    
        [Display(Name = "Group name")]
        public string Name { get; set; }

        public ICollection<UserDTO> Users { get; set; }
    }
}
