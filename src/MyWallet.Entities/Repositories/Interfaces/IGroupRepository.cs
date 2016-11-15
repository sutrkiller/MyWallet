using System;
using System.Threading.Tasks;
using MyWallet.Entities.Models;

namespace MyWallet.Entities.Repositories.Interfaces
{
    public interface IGroupRepository
    {
        /// <summary>
        /// Adds single group
        /// </summary>
        /// <param name="group">New group</param>
        /// <returns>Added group</returns>
        Task<Group> AddGroup(Group group);

        /// <summary>
        /// Returns single group
        /// </summary>
        /// <param name="id">Guid of group</param>
        /// <returns>Group by id</returns>
        Task<Group> GetSingleGroup(Guid id);

        /// <summary>
        /// Returns all groups
        /// </summary>
        /// <returns>All groups</returns>
        Task<Group[]> GetAllGroups();
    }
}