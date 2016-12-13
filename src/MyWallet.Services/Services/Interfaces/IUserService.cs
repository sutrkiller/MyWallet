using System;
using System.Security.Claims;
using System.Threading.Tasks;
using MyWallet.Entities.Models;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Services.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> EnsureUserExists(ClaimsIdentity userClaims);
        Task<UserDTO[]> GetAllUsers();
        Task<UserDTO> EditCurrency(string userEmail, Guid currencyId);

        Task<Guid?> GetUserId(ClaimsIdentity userClaims);
        Task<UserDTO> GetUser(Guid userId);
    }
}