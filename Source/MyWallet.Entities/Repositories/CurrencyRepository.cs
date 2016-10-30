using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MyWallet.Entities.Configuration;
using MyWallet.Entities.Contexts;
using MyWallet.Entities.DataAccessModels;
using MyWallet.Entities.Repositories.Interfaces;

namespace MyWallet.Entities.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private MyWalletContext _context;

        public CurrencyRepository(IOptions<ConnectionOptions> connectionOptions)
        {
            _context = new MyWalletContext(connectionOptions.Value.ConnectionString);
        }

        public async Task<Currency> AddCurrency(Currency currency)
        {
            if (currency == null)
            {
                throw new ArgumentNullException(nameof(currency));
            }
            var addedCurrency = _context.Currencies.Add(currency);
            await _context.SaveChangesAsync();

            return addedCurrency;
        }

        public async Task<Currency> GetSingleCurrency(Guid id)
          => await _context
                .Currencies
                .Where(currency => currency.Id == id)
                .SingleOrDefaultAsync();

        public async Task<Currency[]> GetAllCurrencies()
          => await _context.Currencies.ToArrayAsync();
    }
}