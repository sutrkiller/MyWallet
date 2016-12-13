using System;
using System.Security.Claims;
using System.Threading.Tasks;
using MyWallet.Entities.Models;
using MyWallet.Services.DataTransferModels;
using User = MyWallet.Services.DataTransferModels.User;

namespace MyWallet.Services.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> EnsureUserExists(ClaimsIdentity userClaims);
        Task<User[]> GetAllUsers();
        Task<User> EditCurrency(string userEmail, Guid currencyId);

        Task<Guid?> GetUserId(ClaimsIdentity userClaims);
        Task<User> GetUser(Guid userId);
    }
}