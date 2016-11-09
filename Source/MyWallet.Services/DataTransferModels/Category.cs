using System.Collections.Generic;
using MyWallet.Entities.DataAccessModels;

namespace MyWallet.Services.DataTransferModels
{
    public class Category : BaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Entry> Entries { get; set; } = new HashSet<Entry>();
    }
}