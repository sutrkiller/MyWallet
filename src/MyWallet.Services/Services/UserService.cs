using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using MyWallet.Entities.Models;
using MyWallet.Entities.Repositories.Interfaces;
using MyWallet.Services.DataTransferModels;
using MyWallet.Services.Services.Interfaces;

namespace MyWallet.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ICurrencyRepository _currencyRepository;

        public UserService(IUserRepository userRepository, IMapper mapper, ICurrencyRepository currencyRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _currencyRepository = currencyRepository;
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
                await _userRepository.AddUser(new User
                {
                    Email = userClaims.FindFirst(ClaimTypes.Email)?.Value,
                    Name = userClaims.Name,
                    PreferredCurrency = currency
                });
            }

            return user;
        }

        public async Task<UserDTO[]> GetAllUsers()
        {
            await Task.Delay(0);
            var users = _userRepository.GetAllUsers().ToArrayAsync();
            return _mapper.Map<UserDTO[]>(users);
        }
    }
}