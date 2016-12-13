using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MyWallet.Entities.Models;
using MyWallet.Entities.Repositories.Interfaces;
using MyWallet.Services.DataTransferModels;
using MyWallet.Services.Filters;
using MyWallet.Services.Services.Interfaces;

namespace MyWallet.Services.Services
{
    public class GroupService : IGroupService
    {
        private readonly ILogger<IBudgetService> _logger;
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GroupService(
            IGroupRepository groupRepository,
            IUserRepository userRepository,
            ILogger<IBudgetService> logger,
            IMapper mapper)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<GroupDTO> AddGroup(GroupDTO group, ICollection<Guid> userIds)
        {
            var dataAccessGroupModel = _mapper.Map<Group>(group);
            dataAccessGroupModel.Users = await _userRepository.GetUsersFromIds(userIds).ToArrayAsync();
            dataAccessGroupModel = await _groupRepository.AddGroup(dataAccessGroupModel);
            return _mapper.Map<GroupDTO>(dataAccessGroupModel);
        }

        public async Task<GroupDTO[]> GetAllGroups(GroupFilter filter = null)
        {
            var tmpGroups = _groupRepository.GetAllGroups();
            if (filter != null)
            {
                if (filter.UserId.HasValue)
                {
                    tmpGroups = tmpGroups.Where(x => x.Users.Any(u => u.Id == filter.UserId.Value));
                }
            }

            var groups = await tmpGroups.OrderBy(x => x.Name).ToArrayAsync();
            return _mapper.Map<GroupDTO[]>(groups);
        }

        public async Task<GroupDTO> GetGroup(Guid id)
        {
            var group = await _groupRepository.GetSingleGroup(id);

            return _mapper.Map<GroupDTO>(group);
        }

        public async Task DeleteGroup(Guid id)
        {
            var group = await _groupRepository.GetSingleGroup(id);
            await _groupRepository.DeleteGroup(group);
        }

        public async Task<GroupDTO> EditGroup(GroupDTO groupDto, ICollection<Guid> userIds)
        {
            var model = _mapper.Map<Group>(groupDto);
            model.Users = await _userRepository.GetUsersFromIds(userIds).ToArrayAsync();
            model = await _groupRepository.EditGroup(model);
            return _mapper.Map<GroupDTO>(model);
        }
    }
}
