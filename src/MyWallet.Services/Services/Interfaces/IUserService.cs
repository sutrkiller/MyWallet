using System.Security.Claims;
using System.Threading.Tasks;
using MyWallet.Entities.Models;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Services.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> EnsureUserExists(ClaimsIdentity userClaims);
        Task<UserDTO[]> GetAllUsers();
    }
}