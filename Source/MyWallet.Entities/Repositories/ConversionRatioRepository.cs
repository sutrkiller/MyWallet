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
using MyWallet.Entities.Repositories.Interfaces.MyWallet.Entities.Repositories.Interfaces;

namespace MyWallet.Entities.Repositories
{
    public class ConversionRatioRepository : IConversionRatioRepository
    {
        private MyWalletContext _context;

        public ConversionRatioRepository(IOptions<ConnectionOptions> connectionOptions)
        {
            _context = new MyWalletContext(connectionOptions.Value.ConnectionString);
        }

        public async Task<ConversionRatio> AddConversionRatio(ConversionRatio ratio)
        {
            if (ratio == null)
            {
                throw new ArgumentNullException(nameof(ratio));
            }
            var addedRatio = _context.ConversionRatios.Add(ratio);
            await _context.SaveChangesAsync();

            return addedRatio;
        }

        public async Task<ConversionRatio> GetSingleConversionRatio(Guid id)
           => await _context
                .ConversionRatios
                .Where(ratio => ratio.Id == id)
                .SingleOrDefaultAsync();

        public async Task<ConversionRatio[]> GetAllConversionRatios()
              => await _context.ConversionRatios.ToArrayAsync();
    }
}
