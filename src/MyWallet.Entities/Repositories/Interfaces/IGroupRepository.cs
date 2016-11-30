using System;
using System.Collections.Generic;
using System.Linq;
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
        IQueryable<Group> GetAllGroups();

        /// <summary>
        /// Returns specific groupús
        /// </summary>
        /// <param name="groupIds">Collection of guid of groups</param>
        /// <returns>Groups specified by groupIds</returns>
        IQueryable<Group> GetGroupsFromIds(ICollection<Guid> groupIds);
        Task DeleteGroup(Group group);
        Task<Group> UpdateGroup(Group group);
    }
}