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

        public async Task<GroupDTO[]> GetAllGroups()
        {
            _logger.LogInformation("Starting Group service method");

            //TODO: change this later
            await Task.Delay(0);
            var groups = _groupRepository.GetAllGroups().ToArray();
            return _mapper.Map<GroupDTO[]>(groups);
        }

        public async Task<GroupDTO> GetGroup(Guid id)
        {
            var group = await _groupRepository.GetSingleGroup(id);

            return _mapper.Map<GroupDTO>(group);
        }
    }
}
