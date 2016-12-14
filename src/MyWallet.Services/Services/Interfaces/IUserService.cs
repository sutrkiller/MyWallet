using System;
using System.Security.Claims;
using System.Threading.Tasks;
using User = MyWallet.Services.DataTransferModels.User;

namespace MyWallet.Services.Services.Interfaces
{
    /// <summary>
    /// User service used to access user repository
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Retrieve user by given claims (by email) or creates new one if not found
        /// </summary>
        /// <param name="userClaims">User claims from request</param>
        /// <returns></returns>
        Task<User> EnsureUserExists(ClaimsIdentity userClaims);
        /// <summary>
        /// Get all users in db
        /// </summary>
        /// <returns>All users</returns>
        Task<User[]> GetAllUsers();
        /// <summary>
        /// Edit preffered currency of user
        /// </summary>
        /// <param name="userEmail">Email of target user</param>
        /// <param name="currencyId">New preffered currency id</param>
        /// <returns></returns>
        Task<User> EditCurrency(string userEmail, Guid currencyId);

        /// <summary>
        /// Retrieve id by email (in Claims)
        /// </summary>
        /// <param name="userClaims">Claims from web context</param>
        /// <returns></returns>
        Task<Guid?> GetUserId(ClaimsIdentity userClaims);
        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="userId">Id of existing user</param>
        /// <returns></returns>
        Task<User> GetUser(Guid userId);
    }
}