using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using MyWallet.Entities.Models;
using MyWallet.Entities.Repositories.Interfaces;
using MyWallet.Services.DataTransferModels;
using MyWallet.Services.Services.Interfaces;
using Group = MyWallet.Entities.Models.Group;
using User = MyWallet.Services.DataTransferModels.User;

namespace MyWallet.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly ICurrencyRepository _currencyRepository;

        public UserService(IUserRepository userRepository, IMapper mapper, ICurrencyRepository currencyRepository, IGroupRepository groupRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _currencyRepository = currencyRepository;
            _groupRepository = groupRepository;
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