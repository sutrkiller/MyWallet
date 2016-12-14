using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyWallet.Entities.Models;

namespace MyWallet.Services.DataTransferModels.Mapping
{
    internal class ConversionRatioMappingProfile : Profile
    {
        public ConversionRatioMappingProfile()
       {
           CreateMap<Entities.Models.ConversionRatio, ConversionRatio>()
               .ForMember(d => d.Type, opt => opt.Ignore())
               .MaxDepth(1)
               .ReverseMap();
       }
}
}
