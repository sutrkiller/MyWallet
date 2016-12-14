using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallet.Services.DataTransferModels
{
    /// <summary>
    /// Currency with unique codes
    /// </summary>
    public class Currency : Base
    {
        /// <summary>
        /// Unique code (CZK, EUR...).
        /// </summary>
        public string Code { get; set; }
    }
}
