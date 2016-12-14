using System;
using System.Linq;
using System.Threading.Tasks;
using MyWallet.Entities.Models;

namespace MyWallet.Entities.Repositories.Interfaces
{
    /// <summary>
    /// Repository for accessing entities Currency in db.
    /// </summary>
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
        IQueryable<Currency> GetAllCurrencies();

        /// <summary>
        /// Retrieve currency only by currency code.
        /// </summary>
        /// <param name="code">Code of currency</param>
        /// <returns></returns>
        Task<Currency> GetCurrencyByCode(string code);

        /// <summary>
        /// Returns default currency - CZK
        /// </summary>
        /// <returns></returns>
        Task<Currency> GetDefaultCurrency();
    }
}