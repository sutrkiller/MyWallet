using System;
using System.Collections.Generic;


namespace MyWallet.Services.DataTransferModels
{
    public class Budget : BaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public decimal Amount { get; set; }

        public Currency Currency { get; set; }

        public ICollection<TimePeriod> TimePeriods { get; set; }

        public ICollection<Category> Categories { get; set; }

        public Group Group { get; set; }

    }
}
