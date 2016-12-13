using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWallet.Services.DataTransferModels;
using MyWallet.Services.Filters;

namespace MyWallet.Services.Services.Interfaces
{
    public interface IGroupService
    {
        Task<Group> AddGroup(Group category, ICollection<Guid> userIds);
        Task<Group[]> GetAllGroups(GroupFilter filter = null);
        Task<Group> GetGroup(Guid id);
        Task DeleteGroup(Guid id);
        Task<Group> EditGroup(Group groupDto, ICollection<Guid> userIds);
    }
}
