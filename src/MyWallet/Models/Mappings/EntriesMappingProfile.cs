using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyWallet.Models.Entries;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Models.Mappings
{
    public class EntriesMappingProfile : Profile
    {
        public EntriesMappingProfile()
        {
            CreateMap<EntryDTO, EntryViewModel>().ReverseMap();

            CreateMap<EntryDTO, EntryDetailsViewModel>()
                .ForMember(d=>d.Amount, opt=>opt.MapFrom(m=>$"{m.Amount:0.00} {m.ConversionRatio.CurrencyFrom.Code}"))
                .ForMember(d=>d.AmountInMain, opt=>opt.MapFrom(m=>$"{decimal.Multiply(m.Amount,m.ConversionRatio.Ratio):0.00} {m.ConversionRatio.CurrencyTo.Code}"))
                .ForMember(d => d.UserName, opt => opt.MapFrom(m => m.User.Name))
                .ForMember(d => d.CategoryName, opt => opt.MapFrom(m => m.Category.Name));

        }
    }
}
