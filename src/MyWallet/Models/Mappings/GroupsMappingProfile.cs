using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyWallet.Models.Entries;
using MyWallet.Models.Groups;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Models.Mappings
{
    public class GroupsMappingProfile : Profile
    {
        public GroupsMappingProfile()
        {
            CreateMap<GroupDTO, GroupViewModel>().ReverseMap();
            
            CreateMap<GroupDTO, GroupDetailsViewModel>()
                .ForMember(d => d.UserNames, opt => opt.MapFrom(m => string.Join(", ", m.Users.Select(x => x.Name))));

            CreateMap<CreateGroupViewModel, GroupDTO>()
                .ForMember(d => d.Users, opt => opt.Ignore())
                .ForMember(d => d.Budgets, opt => opt.Ignore())
                .ReverseMap();
                
        }
    }
}
