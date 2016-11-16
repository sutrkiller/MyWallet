using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyWallet.Entities.Models;

namespace MyWallet.Services.DataTransferModels.Mapping
{
    public class ConversionRatioMappingProfile : Profile
    {
        public ConversionRatioMappingProfile()
        {
            CreateMap<ConversionRatio, ConversionRatioDTO>()
                .ForMember(d => d.Type, opt => opt.Ignore())
                .MaxDepth(1)
                .ReverseMap();
        }
    }
}
