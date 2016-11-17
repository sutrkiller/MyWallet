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

        }
    }
}
