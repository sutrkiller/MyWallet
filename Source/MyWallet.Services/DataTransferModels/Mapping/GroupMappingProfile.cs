using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace MyWallet.Services.DataTransferModels.Mapping
{
    public class GroupMappingProfile : Profile
    {
       public GroupMappingProfile()
        {
            CreateMap<Group, Entities.DataAccessModels.Group>()
                .ForMember(dst => dst.Entries, opt => opt.Ignore())
                .ForMember(dst => dst.Users, opt => opt.Ignore())
                .ForMember(dst => dst.Budgets, opt => opt.Ignore());

           CreateMap<Entities.DataAccessModels.Group, Group>();

        }
    }
}
