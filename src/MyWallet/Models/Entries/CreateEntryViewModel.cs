using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Models.Entries
{
    public class CreateEntryViewModel
    {
        public Guid Id { get; set; }
        public string Amount { get; set; }
        public string Description { get; set; }
        public DateTime EntryTime { get; set; }
        [DisplayName("User")]
        public UserDTO User { get; set; }
        [DisplayName("Category")]
        public ICollection<Guid> CategoryIds { get; set; } = new List<Guid>();
        public SelectList CategoriesList { get; set; }
        public ICollection<Guid> BudgetIds { get; set; } = new List<Guid>();
        public SelectList BudgetsList { get; set; }
        public ICollection<Guid> GroupIds { get; set; } = new List<Guid>();
        public SelectList GroupsList { get; set; }
    }
}
