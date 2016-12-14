using AutoMapper;
using MyWallet.Entities.Models;

namespace MyWallet.Services.DataTransferModels.Mapping
{
    internal class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<Entities.Models.Category, Category>()
                .MaxDepth(1)
                .ReverseMap();
        }
    }
}