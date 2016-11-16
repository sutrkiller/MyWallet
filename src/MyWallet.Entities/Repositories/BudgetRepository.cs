using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
                throw new ArgumentNullException(nameof(budget.Categories));
            }
            if (budget.Group == null)
            {
                throw new ArgumentNullException(nameof(budget.Group));
            }

            budget.Group = _context.Groups.Find(budget.Group.Id);
            budget.Currency = _context.Currencies.Find(budget.Currency.Id);
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
        public async Task<Budget[]> GetBudgetsFromIds(ICollection<Guid> budgetIds)
        => await _context
                 .Budgets
                 .Where(r => budgetIds.Contains(r.Id))
                 .ToArrayAsync();

        public async Task<Budget> GetSingleBudget(Guid id)
        => await _context
                .Budgets
                .Where(budget => budget.Id == id)
                .SingleOrDefaultAsync();

        public async Task<Budget[]> GetAllBudgets()
            => await _context.Budgets.ToArrayAsync();
    }
}
