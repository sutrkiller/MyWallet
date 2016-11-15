﻿using System;
using System.Threading.Tasks;
using MyWallet.Entities.Models;

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
        /// Finds the user by email.
        /// </summary>
        /// <param name="email">Email of existing user</param>
        /// <returns></returns>
        Task<User> GetUserByEmail(string email);

        /// <summary>
        /// Returns all users
        /// </summary>
        /// <returns>All users</returns>
        Task<User[]> GetAllUsers();
    }
}