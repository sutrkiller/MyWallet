using System.Linq;
using AutoMapper;
using MyWallet.Models.Budgets;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Models.Mappings
{
    public class BudgetsMappingProfile : Profile
    {
        public BudgetsMappingProfile()
        {
            CreateMap<BudgetDTO, BudgetViewModel>().ReverseMap();
            CreateMap<CreateBudgetViewModel, BudgetDTO>()
                .ForMember(d=>d.Categories,opt=>opt.Ignore())
                .ForMember(d => d.Group, opt => opt.Ignore())
                .ForMember(d => d.Entries, opt => opt.Ignore())
                .ForMember(d => d.ConversionRatio, opt => opt.Ignore());
            CreateMap<BudgetDTO, BudgetDetailsViewModel>()
                .ForMember(d=>d.Categories,opt=>opt.MapFrom(m=>m.Categories)).ReverseMap();
            CreateMap<EditBudgetViewModel, BudgetDTO>()
                .ForMember(d => d.Categories, opt => opt.Ignore())
                .ForMember(d => d.Group, opt => opt.Ignore())
                .ForMember(d => d.Entries, opt => opt.Ignore())
                .ForMember(d => d.ConversionRatio, opt => opt.Ignore());
            CreateMap<BudgetDTO, EditBudgetViewModel>()
                .ForMember(d => d.CurrencyId, opt => opt.MapFrom(m => m.ConversionRatio.CurrencyFrom.Id))
                .ForMember(d => d.CurrenciesList, opt => opt.Ignore())
                .ForMember(d => d.CategoryIds, opt => opt.MapFrom(m => m.Categories.Select(x => x.Id)))
                .ForMember(d => d.CategoriesList, opt => opt.Ignore())
                .ForMember(d => d.GroupId, opt => opt.MapFrom(m => m.Group.Id))
                .ForMember(d => d.GroupsList, opt => opt.Ignore());


        }
    }
}
