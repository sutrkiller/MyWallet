using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace MyWallet.Services.DataTransferModels.Mapping
{
    public class CurrencyMappingProfile : Profile
    {
        public CurrencyMappingProfile()
        {
            CreateMap<Currency, Entities.DataAccessModels.Currency>()
                .ForMember(dst => dst.ConversionRatiosFrom, opt => opt.Ignore())
                .ForMember(dst => dst.ConversionRatiosTo, opt => opt.Ignore());
            CreateMap<Entities.DataAccessModels.Currency, Currency>();
        }
    }
}
