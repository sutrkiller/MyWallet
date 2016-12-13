using System.Linq;
using AutoMapper;
using MyWallet.Models.Categories;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Models.Mappings
{
    public class CategoriesMappingProfile : Profile
    {
        public CategoriesMappingProfile()
        {
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<CreateCategoryViewModel, Category>()
                .ForMember(d => d.Entries, opt => opt.Ignore());
            CreateMap<Category, EditCategoryViewModel>();
            CreateMap<EditCategoryViewModel, Category>()
                .ForMember(d => d.Entries, opt => opt.Ignore());
            CreateMap<Category, CategoryDetailsViewModel>()
                //.ForMember(d => d.Income, opt => opt.MapFrom(m => m.Entries.Where(e => e.Amount > 0 ).Sum(e => e.Amount)))
                //.ForMember(d => d.Expense, opt => opt.MapFrom(m => m.Entries.Where(e => e.Amount < 0).Sum(e => e.Amount)))
                .ForMember(d=>d.Income,opt=>opt.Ignore())
                .ForMember(d=>d.Expense,opt=>opt.Ignore())
                .ForMember(d => d.Balance, opt => opt.Ignore());

        }
    }
}
