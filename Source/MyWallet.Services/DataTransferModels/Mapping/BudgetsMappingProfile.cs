using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace MyWallet.Services.DataTransferModels.Mapping
{
    public class BudgetsMappingProfile : Profile
    {
        public BudgetsMappingProfile()
        {
            CreateMap<Budget, Entities.DataAccessModels.Budget>()
                .ForMember(dst => dst.Entries, opt => opt.Ignore());
            CreateMap<Entities.DataAccessModels.Budget, Budget>();

        }
    }
}