using System;

namespace MyWallet.Services.DataTransferModels
{
    public class TimePeriod : BaseModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}