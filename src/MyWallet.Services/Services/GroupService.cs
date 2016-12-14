using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MyWallet.Entities.Repositories.Interfaces;
using MyWallet.Services.Filters;
using MyWallet.Services.Services.Interfaces;
using Group = MyWallet.Services.DataTransferModels.Group;

namespace MyWallet.Services.Services
{
    internal class GroupService : IGroupService
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

        public async Task<Group> AddGroup(Group group, ICollection<Guid> userIds)
        {
            var dataAccessGroupModel = _mapper.Map<Entities.Models.Group>(group);
            dataAccessGroupModel.Users = await _userRepository.GetUsersFromIds(userIds).ToArrayAsync();
            dataAccessGroupModel = await _groupRepository.AddGroup(dataAccessGroupModel);
            return _mapper.Map<Group>(dataAccessGroupModel);
        }

        public async Task<Group[]> GetAllGroups(GroupFilter filter = null)
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
            return _mapper.Map<Group[]>(groups);
        }

        public async Task<Group> GetGroup(Guid id)
        {
            var group = await _groupRepository.GetSingleGroup(id);

            return _mapper.Map<Group>(group);
        }

        public async Task DeleteGroup(Guid id)
        {
            var group = await _groupRepository.GetSingleGroup(id);
            await _groupRepository.DeleteGroup(group);
        }

        public async Task<Group> EditGroup(Group groupDto, ICollection<Guid> userIds)
        {
            var model = _mapper.Map<Entities.Models.Group>(groupDto);
            model.Users = await _userRepository.GetUsersFromIds(userIds).ToArrayAsync();
            model = await _groupRepository.EditGroup(model);
            return _mapper.Map<Group>(model);
        }
    }
}
