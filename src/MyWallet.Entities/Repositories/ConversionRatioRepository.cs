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
    internal class ConversionRatioRepository : IConversionRatioRepository
    {
        private readonly MyWalletContext _context;

        internal ConversionRatioRepository(MyWalletContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _context = context;
        }

        public ConversionRatioRepository(IOptions<ConnectionOptions> connectionOptions)
             : this(new MyWalletContext(connectionOptions.Value.ConnectionString))
        {

        }

        public async Task<ConversionRatio> AddConversionRatio(ConversionRatio ratio)
        {
            if (ratio == null)
            {
                throw new ArgumentNullException(nameof(ratio));
            }
            ratio.CurrencyFrom = _context.Currencies.Find(ratio.CurrencyFrom.Id);
            ratio.CurrencyTo = _context.Currencies.Find(ratio.CurrencyTo.Id);
            var addedRatio = _context.ConversionRatios.Add(ratio);
            await _context.SaveChangesAsync();

            return addedRatio;
        }

        public async Task<ConversionRatio> GetSingleConversionRatio(Guid id)
           => await _context
                .ConversionRatios
                .Where(ratio => ratio.Id == id)
                .SingleOrDefaultAsync();

        public IQueryable<ConversionRatio> GetAllConversionRatios()
              => _context.ConversionRatios.AsQueryable();
    }
}