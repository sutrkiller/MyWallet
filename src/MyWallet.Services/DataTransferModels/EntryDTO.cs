using System;
using System.Collections.Generic;
using MyWallet.Entities.Models;

namespace MyWallet.Services.DataTransferModels
{
    public class EntryDTO : BaseDTO
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime EntryTime { get; set; }
        public UserDTO User { get; set; }
        public CategoryDTO Category { get; set; }
        public virtual ConversionRatio ConversionRatio { get; set; }
    }
}