using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using MyWallet.Entities.Repositories.Interfaces;
using MyWallet.Services.Services.Interfaces;
using Group = MyWallet.Entities.Models.Group;
using User = MyWallet.Services.DataTransferModels.User;

namespace MyWallet.Services.Services
{
    internal class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IBudgetRepository _budgetRepository;

        public UserService(IUserRepository userRepository, IMapper mapper, ICurrencyRepository currencyRepository, IGroupRepository groupRepository, IBudgetRepository budgetRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _currencyRepository = currencyRepository;
            _groupRepository = groupRepository;
            _budgetRepository = budgetRepository;
        }

        public async Task<User> EnsureUserExists(ClaimsIdentity userClaims)
        {
            if (userClaims == null)
            {
                throw new ArgumentNullException(nameof(userClaims));
            }

            var currency = await _currencyRepository.GetDefaultCurrency();

            var user = await _userRepository.GetUserByEmail(userClaims.FindFirst(ClaimTypes.Email)?.Value);
            if (user == null)
            {
                user = await _userRepository.AddUser(new Entities.Models.User
                {
                    Email = userClaims.FindFirst(ClaimTypes.Email)?.Value,
                    Name = userClaims.Name,
                    PreferredCurrency = currency
                });

                
                var group = new Group() {Name = user.Name,Users = new HashSet<Entities.Models.User>() {user} };
                group = await _groupRepository.AddGroup(group);
                var cFrom = await _currencyRepository.GetCurrencyByCode("CZK");
                var cr = cFrom.ConversionRatiosFrom.FirstOrDefault() ??
                         new Entities.Models.ConversionRatio
                         {
                             CurrencyFrom = cFrom,
                             CurrencyTo = cFrom,
                             Date = DateTime.Now,
                             Ratio = 1m,
                             Type = "UserCreation"
                         };
                var budget = new Entities.Models.Budget { Amount = 10000m, Description = "Sample budget for genral purpose", Name = "General", StartDate = DateTime.Today, EndDate = DateTime.Today.AddYears(1), ConversionRatio = cr };
                budget.Group = group;
                budget = await _budgetRepository.AddBudget(budget);
                //await _budgetRepository.AddBudget(budget);
            }

            return _mapper.Map<User>(user);
        }

        public async Task<User[]> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers().ToArrayAsync();
            return _mapper.Map<User[]>(users);
        }

        public async Task<User> EditCurrency(string userEmail, Guid currencyId)
        {
            if (string.IsNullOrEmpty(userEmail))
            {
                throw new ArgumentNullException(nameof(userEmail));
            }
            var user = await _userRepository.GetUserByEmail(userEmail);
            if (user == null) throw new ArgumentException("User with this email not found.");
            var currency = await _currencyRepository.GetSingleCurrency(currencyId);
            if (currency == null) throw new ArgumentException("Currency not found.");
            user.PreferredCurrency = currency;

            var result = await _userRepository.EditUser(user);
            return _mapper.Map<User>(result);
        }

        public async Task<Guid?> GetUserId(ClaimsIdentity userClaims)
        {
            if (userClaims == null)
            {
                throw new ArgumentNullException(nameof(userClaims));
            }

            return (await _userRepository.GetUserByEmail(userClaims.FindFirst(ClaimTypes.Email)?.Value))?.Id;
        }

        public async Task<User> GetUser(Guid userId)
        {
            var user = await _userRepository.GetSingleUser(userId);
            return user == null ? null : _mapper.Map<User>(user);
        }
    }
}