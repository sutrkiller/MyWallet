using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyWallet.Entities.DataAccessModels;

namespace MyWallet.Entities.Repositories.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Adds single user
        /// </summary>
        /// <param name="user">New user</param>
        /// <returns>Added user</returns>
        Task<User> AddUser(User user);

        /// <summary>
        /// Returns single user
        /// </summary>
        /// <param name="id">Guid of user</param>
        /// <returns>User by id</returns>
        Task<User> GetSingleUser(Guid id);

        /// <summary>
        /// Returns all users
        /// </summary>
        /// <returns>All users</returns>
        Task<User[]> GetAllUsers();
    }
}