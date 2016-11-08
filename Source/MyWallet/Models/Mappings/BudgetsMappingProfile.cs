using AutoMapper;
using MyWallet.Models.Budgets;

namespace MyWallet.Models.Mappings
{
    public class BudgetsMappingProfile : Profile
    {
        public BudgetsMappingProfile()
        {
            CreateMap<Services.DataTransferModels.Budget, BudgetViewModel>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dst => dst.Amount, opt => opt.MapFrom(src => src.Amount));

            CreateMap<CreateBudgetViewModel, Services.DataTransferModels.Budget>()
                 .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                 .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
                 .ForMember(dst => dst.Amount, opt => opt.MapFrom(src => src.Amount));

            CreateMap<Services.DataTransferModels.Budget,CreateBudgetViewModel>()
                 .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                 .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
                 .ForMember(dst => dst.Amount, opt => opt.MapFrom(src => src.Amount));

            CreateMap<Services.DataTransferModels.Budget, BudgetDetailsViewModel>()
                 .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                 .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
                 .ForMember(dst => dst.Amount, opt => opt.MapFrom(src => src.Amount));
        }
    }
}
