﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MyWallet.Models.Categories
{
    public class CreateCategoryViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
