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
    public class TimePeriodRepository : ITimePeriodRepository
    {
        private MyWalletContext _context;

        public TimePeriodRepository(IOptions<ConnectionOptions> connectionOptions)
        {
            _context = new MyWalletContext(connectionOptions.Value.ConnectionString);
        }

        public async Task<TimePeriod> AddTimePeriod(TimePeriod period)
        {
            if (period == null)
            {
                throw new ArgumentNullException(nameof(period));
            }
            var addedPeriod = _context.TimePeriods.Add(period);
            await _context.SaveChangesAsync();

            return addedPeriod;
        }

        public async Task<TimePeriod> GetSingleTimePeriod(Guid id)
           => await _context
                .TimePeriods
                .Where(period => period.Id == id)
                .SingleOrDefaultAsync();

        public async Task<TimePeriod[]> GetAllTimePeriods()
            => await _context.TimePeriods.ToArrayAsync();
        
    }
}