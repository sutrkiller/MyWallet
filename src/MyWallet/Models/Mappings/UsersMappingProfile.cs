﻿using System;
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
            CreateMap<UserDTO, ManageUserCreateViewModel>()
                .ForMember(d => d.CurrencyId, opt => opt.MapFrom(m => m.PreferredCurrency.Id))
                .ForMember(d => d.OriginalCurrencyId, opt => opt.MapFrom(m => m.PreferredCurrency.Id))
                .ForMember(d => d.CurrenciesList, opt => opt.Ignore())
                .ForMember(d => d.Groups, opt => opt.MapFrom(m => m.Groups));

        }
    }
}
