using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MyWallet.Entities.Configuration;
using MyWallet.Entities.Contexts;
using MyWallet.Entities.DataAccessModels;
using MyWallet.Entities.Repositories.Interfaces;

namespace MyWallet.Entities.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly MyWalletContext _context;

        internal BudgetRepository(MyWalletContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _context = context;
        }

        public BudgetRepository(IOptions<ConnectionOptions> connectionOptions)
             : this(new MyWalletContext(connectionOptions.Value.ConnectionString))
        {

        }

        public async Task<Budget> AddBudget(Budget budget)
        {
            if (budget == null)
            {
                throw new ArgumentNullException(nameof(budget));
            }           
            var addedBudget = _context.Budgets.Add(budget);
            await _context.SaveChangesAsync();

            return addedBudget;
        }

        public async Task<Budget> GetSingleBudget(Guid id)
          => await _context
                .Budgets
                .Where(budget => budget.Id == id)
                .SingleOrDefaultAsync();

        public async Task<Budget[]> GetAllBudgets()
             => await _context.Budgets.ToArrayAsync();
    }
}
