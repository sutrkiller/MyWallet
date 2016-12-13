using AutoMapper;
using MyWallet.Entities.Models;

namespace MyWallet.Services.DataTransferModels.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<Entities.Models.User, User>()
                .MaxDepth(1)
                .ReverseMap();
        }
    }
}