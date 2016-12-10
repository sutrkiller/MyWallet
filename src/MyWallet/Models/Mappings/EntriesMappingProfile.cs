using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyWallet.Helpers;
using MyWallet.Models.Entries;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Models.Mappings
{
    public class EntriesMappingProfile : Profile
    {
        public EntriesMappingProfile()
        {
            CreateMap<EntryDTO, EntryViewModel>()
                .ForMember(d=>d.Currency,opt=>opt.MapFrom(x=>x.ConversionRatio.CurrencyFrom.Code))
                .ReverseMap();

            CreateMap<EntryDTO, EntryDetailsViewModel>()
                .ForMember(d => d.Amount, opt => opt.MapFrom(m => m.Amount.FormatCurrency(m.ConversionRatio.CurrencyFrom.Code)))
                .ForMember(d => d.AmountInMain, opt => opt.MapFrom(m => decimal.Multiply(m.Amount, m.ConversionRatio.Ratio).FormatCurrency(m.ConversionRatio.CurrencyTo.Code)))
                .ForMember(d => d.UserName, opt => opt.MapFrom(m => m.User.Name))
                .ForMember(d => d.CategoryNames, opt => opt.MapFrom(m => string.Join(", ", m.Categories.Select(x => x.Name))))
                .ForMember(d => d.BudgetsNames, opt => opt.MapFrom(m => string.Join(", ", m.Budgets.Select(x => x.Name))));

            CreateMap<CreateEntryViewModel, EntryDTO>()
                .ForMember(d => d.User, opt => opt.Ignore())
                .ForMember(d => d.Categories, opt => opt.Ignore())
                .ForMember(d => d.ConversionRatio, opt => opt.Ignore())
                .ForMember(d => d.Budgets, opt => opt.Ignore());

            CreateMap<EntryDTO, EditEntryViewModel>()
                .ForMember(d => d.CurrencyId, opt => opt.MapFrom(m => m.ConversionRatio.CurrencyFrom.Id))
                .ForMember(d => d.CurrenciesList, opt => opt.Ignore())
                .ForMember(d => d.CategoryIds, opt => opt.MapFrom(m => m.Categories.Select(x => x.Id)))
                .ForMember(d => d.CategoriesList, opt => opt.Ignore())
                .ForMember(d => d.BudgetIds, opt => opt.MapFrom(m => m.Budgets.Select(x => x.Id)))
                .ForMember(d => d.BudgetsList, opt => opt.Ignore())
                .ForMember(d => d.ConversionRatioId, opt => opt.MapFrom(m => m.ConversionRatio.Id))
                .ForMember(d => d.ConversionRatiosList, opt => opt.Ignore())
                .ForMember(d => d.CustomRatioAmount, opt => opt.Ignore())
                .ForMember(d => d.CustomRatioCurrencyId, opt => opt.Ignore())
                .ForMember(d => d.IsIncome, opt => opt.Ignore())
                .ForMember(d => d.CustomCurrenciesList, opt => opt.Ignore());


            CreateMap<EditEntryViewModel, EntryDTO>()
                .ForMember(d => d.User, opt => opt.Ignore())
                .ForMember(d => d.Categories, opt => opt.Ignore())
                .ForMember(d => d.ConversionRatio, opt => opt.Ignore())
                .ForMember(d => d.Budgets, opt => opt.Ignore());

        }
    }
}
