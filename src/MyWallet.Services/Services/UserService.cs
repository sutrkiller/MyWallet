using System;
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
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
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

        public async Task<UserDTO[]> GetAllUsers()
        {
            //TODO: change this later
            await Task.Delay(0);
            var users = _userRepository.GetAllUsers().ToArray();
            return _mapper.Map<UserDTO[]>(users);
        }
    }
}