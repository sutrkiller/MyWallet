using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWallet.Entities.Models;

namespace MyWallet.Services.DataTransferModels
{
    public class CategoryDTO : BaseDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
