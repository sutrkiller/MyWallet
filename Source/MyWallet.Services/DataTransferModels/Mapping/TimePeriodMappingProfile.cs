using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace MyWallet.Services.DataTransferModels.Mapping
{
    public class TimePeriodMappingProfile : Profile
    {

        public TimePeriodMappingProfile()
        {
                
            CreateMap<TimePeriod, Entities.DataAccessModels.TimePeriod>()
                .ForMember(dst => dst.Budget, opt => opt.Ignore());
            CreateMap<Entities.DataAccessModels.TimePeriod, TimePeriod>();
        }

    }
    
}
