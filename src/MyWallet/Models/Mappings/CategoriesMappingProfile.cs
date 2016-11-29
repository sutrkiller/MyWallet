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
            CreateMap<CategoryDTO, CategoryViewModel>().ReverseMap();
            CreateMap<CreateCategoryViewModel, CategoryDTO>()
                .ForMember(d => d.Entries, opt => opt.Ignore());
            CreateMap<CategoryDTO, EditCategoryViewModel>();
            CreateMap<EditCategoryViewModel, CategoryDTO>()
                .ForMember(d => d.Entries, opt => opt.Ignore());
            CreateMap<CategoryDTO, CategoryDetailsViewModel>()
                .ForMember(d => d.Income, opt => opt.MapFrom(m => m.Entries.Where(e => e.Amount > 0 ).Sum(e => e.Amount)))
                .ForMember(d => d.Expence, opt => opt.MapFrom(m => m.Entries.Where(e => e.Amount < 0).Sum(e => e.Amount)))
                .ForMember(d => d.Balance, opt => opt.MapFrom(m => m.Entries.Sum(e => e.Amount)));

        }
    }
}
