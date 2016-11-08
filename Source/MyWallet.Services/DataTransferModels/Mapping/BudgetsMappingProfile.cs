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

            CreateMap<Entities.DataAccessModels.Budget, Budget>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dst => dst.Amount, opt => opt.MapFrom(src => src.Amount));
            CreateMap<Budget,Entities.DataAccessModels.Budget>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dst => dst.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dst => dst.Currency, opt => opt.Ignore())
                .ForMember(dst => dst.Categories, opt => opt.Ignore())
                .ForMember(dst => dst.Entries, opt => opt.Ignore())
                .ForMember(dst => dst.TimePeriods, opt => opt.Ignore())
                .ForMember(dst => dst.ConversionRatio, opt => opt.Ignore())
                .ForMember(dst => dst.Group, opt => opt.Ignore())
                .ForMember(dst => dst.Id, opt => opt.Ignore());
        }
    
    }
}
