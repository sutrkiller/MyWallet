using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallet.Services.DataTransferModels
{
    public class Group : Base
    {
        public string Name { get; set; }
        public ICollection<Budget> Budgets { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
