using AutoMapper;
using MyWallet.Entities.Models;

namespace MyWallet.Services.DataTransferModels.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDTO>()
                .MaxDepth(1)
                .ReverseMap();
        }
    }
}