using AutoMapper;
using MyWallet.Models.Budgets;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Models.Mappings
{
    public class BudgetsMappingProfile : Profile
    {
        public BudgetsMappingProfile()
        {
            CreateMap<Budget, BudgetViewModel>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dst => dst.Amount, opt => opt.MapFrom(src => src.Amount));

            CreateMap<Budget, CreateBudgetViewModel>().ReverseMap();
            CreateMap<CreateBudgetViewModel, Budget>().ReverseMap();
            CreateMap<Budget, BudgetDetailsViewModel>()
                 .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                 .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
                 .ForMember(dst => dst.Amount, opt => opt.MapFrom(src => src.Amount));
        }
    }
}
