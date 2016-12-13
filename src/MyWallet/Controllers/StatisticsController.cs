using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWallet.Helpers;
using MyWallet.Models.Statistics;
using MyWallet.Services.Filters;
using MyWallet.Services.Services.Interfaces;

namespace MyWallet.Controllers
{
    [Authorize]
    public class StatisticsController : Controller
    {
        private readonly IEntryService _entryService;
        private readonly IUserService _userService;

        public StatisticsController(IEntryService entryService, IUserService userService)
        {
            _entryService = entryService;
            _userService = userService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {

            var model = new StatsViewModel
            {
                Expenses = await GetExpenses(User.Identity as ClaimsIdentity),
                Income = await GetIncome(User.Identity as ClaimsIdentity)
            };

            return View(model);
        }

        public async Task<IncomeExpenseViewModel> GetExpenses(ClaimsIdentity userIdentity)
        {
            return new IncomeExpenseViewModel
            {
                ViewName = "Expenses",
                Header = "You spent:",
                Today = await GetExpensesForPeriod(TimePeriod.Day, userIdentity),
                LastWeek = await GetExpensesForPeriod(TimePeriod.Week, userIdentity),
                LastMonth = await GetExpensesForPeriod(TimePeriod.Month, userIdentity),
                LastYear = await GetExpensesForPeriod(TimePeriod.Year, userIdentity)
            };
        }

        private async Task<IncomeExpenseViewModel> GetIncome(ClaimsIdentity userIdentity)
        {
            return new IncomeExpenseViewModel
            {
                ViewName = "Income",
                Header = "Your income for:",
                Today = await GetIncomesForPeriod(TimePeriod.Day, userIdentity),
                LastWeek = await GetIncomesForPeriod(TimePeriod.Week, userIdentity),
                LastMonth = await GetIncomesForPeriod(TimePeriod.Month, userIdentity),
                LastYear = await GetIncomesForPeriod(TimePeriod.Year, userIdentity)
            };
        }

        private async Task<SpentModel> GetExpensesForPeriod(TimePeriod period, ClaimsIdentity userIdentity)
        {
            var user = await _userService.EnsureUserExists(userIdentity);
            var currency = await _entryService.GetConversionRatiosForCurrency(user.PreferredCurrency.Id);
            var preferedCurrency = currency.OrderByDescending(x => x.Date).FirstOrDefault();
            var entries = await _entryService.GetAllEntries(GetFilter(period, user.Id));

            return new SpentModel
            {
                Amount =
                    entries.Where(x => x.Amount < 0)
                        .Sum(e => e.ToBudgetCurrency(preferedCurrency?.Ratio ?? 1m)),
                CurrencyCode = user.PreferredCurrency.Code
            };
        }

        private async Task<SpentModel> GetIncomesForPeriod(TimePeriod period, ClaimsIdentity userIdentity)
        {
            var user = await _userService.EnsureUserExists(userIdentity);
            var currency = await _entryService.GetConversionRatiosForCurrency(user.PreferredCurrency.Id);
            var preferedCurrency = currency.OrderByDescending(x => x.Date).FirstOrDefault();
            var entries = await _entryService.GetAllEntries(GetFilter(period, user.Id));

            return new SpentModel
            {
                Amount =
                    entries.Where(x => x.Amount > 0)
                        .Sum(e => e.ToBudgetCurrency(preferedCurrency?.Ratio ?? 1m)),
                CurrencyCode = user.PreferredCurrency.Code
            };
        }

        private EntriesFilter GetFilter(TimePeriod period, Guid userId)
        {
            var now = DateTime.Now.Date;
            var filter = new EntriesFilter { From = now, To = now, UserId = userId };

            switch (period)
            {
                case TimePeriod.Week:
                    filter.From = now.AddDays(-7);
                    return filter;
                case TimePeriod.Month:
                    filter.From = now.AddMonths(-1);
                    return filter;
                case TimePeriod.Year:
                    filter.From = now.AddYears(-1);
                    return filter;
                default:
                    return filter;
            }
        }

        private enum TimePeriod
        {
            Day,
            Week,
            Month,
            Year
        };
    }
}