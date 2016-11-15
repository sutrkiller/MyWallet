using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MyWallet.Entities.Configuration;
using MyWallet.Entities.Contexts;
using MyWallet.Entities.Models;
using MyWallet.Entities.Repositories.Interfaces;

namespace MyWallet.Entities.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly MyWalletContext _context;

        internal CurrencyRepository(MyWalletContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _context = context;
        }

        public CurrencyRepository(IOptions<ConnectionOptions> connectionOptions)
             : this(new MyWalletContext(connectionOptions.Value.ConnectionString))
        {

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