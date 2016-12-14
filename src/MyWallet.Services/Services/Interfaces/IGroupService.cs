using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyWallet.Services.DataTransferModels;
using MyWallet.Services.Filters;

namespace MyWallet.Services.Services.Interfaces
{
    /// <summary>
    /// Group service. Layer above group repository
    /// </summary>
    public interface IGroupService
    {
        /// <summary>
        /// Create new group
        /// </summary>
        /// <param name="group">Group with filled values</param>
        /// <param name="userIds">Ids of existing user</param>
        /// <returns></returns>
        Task<Group> AddGroup(Group group, ICollection<Guid> userIds);
        /// <summary>
        /// Return all groups by filter
        /// </summary>
        /// <param name="filter">Custom filter</param>
        /// <returns>Filtered groups</returns>
        Task<Group[]> GetAllGroups(GroupFilter filter = null);
        /// <summary>
        /// Get single group by id
        /// </summary>
        /// <param name="id">Id of existing group</param>
        /// <returns></returns>
        Task<Group> GetGroup(Guid id);
        /// <summary>
        /// Delete group by id
        /// </summary>
        /// <param name="id">Id of existing group</param>
        /// <returns></returns>
        Task DeleteGroup(Guid id);
        /// <summary>
        /// Edit group
        /// </summary>
        /// <param name="groupDto">Group with new values and valid id</param>
        /// <param name="userIds">Ids of existing users</param>
        /// <returns></returns>
        Task<Group> EditGroup(Group groupDto, ICollection<Guid> userIds);
    }
}
