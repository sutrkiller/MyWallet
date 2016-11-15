using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyWallet.Entities.Models;

namespace MyWallet.Services.DataTransferModels.Mapping
{
    public class CurrencyMappingProfile : Profile
    {
        public CurrencyMappingProfile()
        {
            CreateMap<Currency, CurrencyDTO>()
                .ReverseMap();
        }
    }
}
