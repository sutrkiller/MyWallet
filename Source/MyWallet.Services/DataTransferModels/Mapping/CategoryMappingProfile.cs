using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace MyWallet.Services.DataTransferModels.Mapping
{
    public class CategoryMappingProfile : Profile
    {
       public CategoryMappingProfile()
        {
            CreateMap<Category, Entities.DataAccessModels.Category>()
                .ForMember(dst => dst.Entries, opt => opt.Ignore())
                .ForMember(dst => dst.Budgets, opt => opt.Ignore());
           CreateMap<Entities.DataAccessModels.Category, Category>();
        }
    }
}
