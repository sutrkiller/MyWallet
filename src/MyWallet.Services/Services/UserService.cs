using System;
using System.Security.Claims;
using System.Threading.Tasks;
using MyWallet.Entities.Models;
using MyWallet.Entities.Repositories.Interfaces;
using MyWallet.Services.Services.Interfaces;

namespace MyWallet.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> EnsureUserExists(ClaimsIdentity userClaims)
        {
            if (userClaims == null)
            {
                throw new ArgumentNullException(nameof(userClaims));
            }

            var gamer = await _userRepository.GetUserByEmail(userClaims.FindFirst(ClaimTypes.Email)?.Value) ??
                        await _userRepository.AddUser(new User
                        {
                            Email = userClaims.FindFirst(ClaimTypes.Email)?.Value,
                            Name = userClaims.Name,
                        });

            return gamer;
        }
    }
}