using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWallet.Entities.Models;

namespace MyWallet.Entities.Repositories.Interfaces
{
    public interface IConversionRatioRepository
    {
        /// <summary>
        /// Adds single conversion ratio
        /// </summary>
        /// <param name="ratio">New conversion ratio</param>
        /// <returns>Added conversion ratio</returns>
        Task<ConversionRatio> AddConversionRatio(ConversionRatio ratio);

        /// <summary>
        /// Returns single conversion ratio
        /// </summary>
        /// <param name="id">Guid of conversion ratio</param>
        /// <returns>Conversion ratio by id</returns>
        Task<ConversionRatio> GetSingleConversionRatio(Guid id);

        /// <summary>
        /// Returns all conversion ratios
        /// </summary>
        /// <returns>All conversion ratios</returns>
        Task<ConversionRatio[]> GetAllConversionRatios();
    }
}
