using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyWallet.Models.Users;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Models.Mappings
{
    public class UsersMappingProfile : Profile
    {
        public UsersMappingProfile()
        {
            CreateMap<User, ManageUserCreateViewModel>()
                .ForMember(d => d.CurrencyId, opt => opt.MapFrom(m => m.PreferredCurrency.Id))
                .ForMember(d => d.OriginalCurrencyId, opt => opt.MapFrom(m => m.PreferredCurrency.Id))
                .ForMember(d => d.CurrenciesList, opt => opt.Ignore())
                .ForMember(d => d.Groups, opt => opt.MapFrom(m => string.Join("\n", m.Groups.Select(x => x.Name))))
                .ForMember(d => d.NumberGroups, opt => opt.MapFrom(m => m.Groups.Count));

        }
    }
}
