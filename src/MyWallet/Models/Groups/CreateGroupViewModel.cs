using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Models.Groups
{
    public class CreateGroupViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Users")]
        public ICollection<Guid> UserIds { get; set; } = new List<Guid>();
        public SelectList UsersList { get; set; }
    }
}
