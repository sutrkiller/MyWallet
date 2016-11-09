using AutoMapper;
using MyWallet.Models.Budgets;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Models.Mappings
{
    public class BudgetsMappingProfile : Profile
    {
        public BudgetsMappingProfile()
        {
            CreateMap<Budget, BudgetViewModel>();

            CreateMap<Budget, CreateBudgetViewModel>()
                 .ForMember(dst => dst.CategoriesList, opt => opt.Ignore())
                 .ForMember(dst => dst.CurrencyList, opt => opt.Ignore())
                 .ForMember(dst => dst.CurrencyId, opt => opt.Ignore())
                 .ForMember(dst => dst.CategoryIds, opt => opt.Ignore());

            CreateMap<CreateBudgetViewModel, Budget>()
                .ForMember(dst => dst.Currency, opt => opt.Ignore())
                .ForMember(dst => dst.Categories, opt => opt.Ignore());
            CreateMap<Budget, BudgetDetailsViewModel>().ReverseMap();
        }
    }
}
