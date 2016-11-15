using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyWallet.Entities.DataAccessModels;

namespace MyWallet.Entities.Repositories.Interfaces
{
    public interface ICurrencyRepository
    {
        /// <summary>
        /// Adds single currency
        /// </summary>
        /// <param name="currency">New currency</param>
        /// <returns>Added currency</returns>
        Task<Currency> AddCurrency(Currency currency);

        /// <summary>
        /// Returns single currency
        /// </summary>
        /// <param name="id">Guid of currency</param>
        /// <returns>Currency by id</returns>
        Task<Currency> GetSingleCurrency(Guid id);

        /// <summary>
        /// Returns all currencies
        /// </summary>
        /// <returns>All currencies</returns>
        Task<Currency[]> GetAllCurrencies();
    }
}