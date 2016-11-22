using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MyWallet.Entities.Configuration;
using MyWallet.Entities.Contexts;
using MyWallet.Entities.Models;
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
            if (budget.Categories == null)
            {
                throw new ArgumentNullException(nameof(Budget.Categories));
            }
            if (budget.Group == null)
            {
                throw new ArgumentNullException(nameof(Budget.Group));
            }

            budget.Group = _context.Groups.Find(budget.Group.Id);

            //TODO: change this
            budget.ConversionRatio = _context.ConversionRatios.Add(new ConversionRatio()
            {
                CurrencyFrom = await _context.Currencies.SingleOrDefaultAsync(x => x.Code == "USD"),
                CurrencyTo = await _context.Currencies.SingleOrDefaultAsync(x => x.Code == "EUR"),
                Date = DateTime.Today,
                Ratio = 1.5m
            });

            var categs = budget.Categories;
            budget.Categories = new List<Category>();
            foreach (var cat in categs)
            {
                budget.Categories.Add(_context.Categories.Find(cat.Id));
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

        public IQueryable<Budget> GetAllBudgets()
            => _context.Budgets.AsQueryable();

        public async Task<Budget[]> GetBudgetsFromIds(ICollection<Guid> budgetIDs)
            => await _context.Budgets.Where(b => budgetIDs.Any(id => id == b.Id)).ToArrayAsync();

        public async Task DeleteBudget(Budget budget)
        {
            _context.Budgets.Remove(budget);
            await _context.SaveChangesAsync();
        }
            
    }
}
