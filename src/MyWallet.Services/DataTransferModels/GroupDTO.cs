using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallet.Services.DataTransferModels
{
    public class GroupDTO : BaseDTO
    {
        public string Name { get; set; }
        public ICollection<BudgetDTO> Budgets { get; set; }
        public ICollection<UserDTO> Users { get; set; }
    }
}
