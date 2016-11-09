using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallet.Services.DataTransferModels
{
    public class Budget
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public decimal Amount { get; set; }

    }
}
