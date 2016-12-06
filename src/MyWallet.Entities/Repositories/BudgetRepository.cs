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
                throw new ArgumentNullException(nameof(Budget.Categories));
            }
            if (budget.Group == null)
            {
                throw new ArgumentNullException(nameof(Budget.Group));
            }
            if (budget.ConversionRatio == null)
            {
                throw new ArgumentNullException(nameof(Budget.ConversionRatio));
            }

            budget.Group = _context.Groups.Find(budget.Group.Id);
            budget.ConversionRatio = _context.ConversionRatios.Find(budget.ConversionRatio.Id);
            
            var categories = budget.Categories;
            budget.Categories = new List<Category>();
            foreach (var cat in categories)
            {
                budget.Categories.Add(_context.Categories.Find(cat.Id));
            }
            var addedBudget = _context.Budgets.Add(budget);
            await _context.SaveChangesAsync();

            return addedBudget;
        }

        public async Task EditBudget(Budget budget)
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
            if (budget.ConversionRatio == null)
            {
                throw new ArgumentNullException(nameof(Budget.ConversionRatio));
            }
            var local = await _context.Budgets.FindAsync(budget.Id);
            _context.Entry(local).CurrentValues.SetValues(budget);
            local.Group = await _context.Groups.FindAsync(budget.Group.Id);
            local.ConversionRatio = await _context.ConversionRatios.FindAsync(budget.ConversionRatio.Id);
            foreach (var category in local.Categories)
            {
                category.Budgets = category.Budgets.Where(x => x.Id != local.Id).ToList();
            }
            var categories = budget.Categories.Select(x => _context.Categories.Find(x.Id)).ToList();
            local.Categories = new HashSet<Category>(categories);
            await _context.SaveChangesAsync();
        }

        public async Task<Budget> GetSingleBudget(Guid id)
        => await _context
                .Budgets
                .Where(budget => budget.Id == id)
                .SingleOrDefaultAsync();

        public IQueryable<Budget> GetAllBudgets()
            => _context.Budgets.AsQueryable();

        public IQueryable<Budget> GetBudgetsFromIds(ICollection<Guid> budgetIDs)
            => _context.Budgets.Where(b => budgetIDs.Any(id => id == b.Id));

        public async Task DeleteBudget(Budget budget)
        {
            _context.Budgets.Remove(budget);
            await _context.SaveChangesAsync();
        }
            
    }
}
