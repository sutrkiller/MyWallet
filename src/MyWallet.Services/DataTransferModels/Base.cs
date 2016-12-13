using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallet.Services.DataTransferModels
{
    /// <summary>
    /// Base class for services. Adding commong id property.
    /// </summary>
    public class Base
    {
        /// <summary>
        /// Id of models. Same as db ID.
        /// </summary>
        public Guid Id { get; set; }
    }
}
