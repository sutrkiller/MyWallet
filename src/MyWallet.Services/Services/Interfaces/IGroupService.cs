using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Services.Services.Interfaces
{
    public interface IGroupService
    {
        Task<GroupDTO> AddGroup(GroupDTO category, ICollection<Guid> userIds);
        Task<GroupDTO[]> GetAllGroups();
        Task<GroupDTO> GetGroup(Guid id);
        Task DeleteGroup(Guid id);
        Task<GroupDTO> EditGroup(GroupDTO groupDto, ICollection<Guid> userIds);
    }
}
