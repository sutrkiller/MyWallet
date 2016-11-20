using System;
using System.Collections.Generic;
using System.ComponentModel;
using MyWallet.Entities.Models;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Models.Categories
{
    public class CategoryDetailsViewModel
    {
        [DisplayName("Category name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Income { get; set; }
        public string Expence { get; set; }
        public string Balance { get; set; }
    }
}