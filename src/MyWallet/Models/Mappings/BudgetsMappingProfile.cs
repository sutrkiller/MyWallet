using System;
using System.Globalization;
using System.Linq;
using AutoMapper;
using MyWallet.Helpers;
using MyWallet.Models.Budgets;
using MyWallet.Services.DataTransferModels;

namespace MyWallet.Models.Mappings
{
    public class BudgetsMappingProfile : Profile
    {
        public BudgetsMappingProfile()
        {
            CreateMap<Budget, BudgetViewModel>()
                .ForMember(d=>d.CurrencyCode,opt=>opt.MapFrom(m=>m.ConversionRatio.CurrencyFrom.Code))
                .ReverseMap();
            CreateMap<CreateBudgetViewModel, Budget>()
                .ForMember(d=>d.Categories,opt=>opt.Ignore())
                .ForMember(d => d.Group, opt => opt.Ignore())
                .ForMember(d => d.Entries, opt => opt.Ignore())
                .ForMember(d => d.ConversionRatio, opt => opt.Ignore())
                .ForMember(d=>d.StartDate,opt=>opt.MapFrom(m=>DateTime.ParseExact(m.StartDate,"MM/dd/yyyy",new CultureInfo("en-US"))))
                .ForMember(d => d.EndDate, opt => opt.MapFrom(m => DateTime.ParseExact(m.EndDate, "MM/dd/yyyy", new CultureInfo("en-US"))));

            CreateMap<Budget, BudgetDetailsViewModel>()
                .ForMember(d => d.Categories, opt => opt.MapFrom(m => m.Categories))
                .ForMember(d => d.NumberOfEntries, opt => opt.MapFrom(m => m.Entries.Count))
                .ForMember(d => d.Currency, opt => opt.MapFrom(m => m.ConversionRatio.CurrencyFrom.Code))
                .ForMember(d => d.Entries, opt => opt.Ignore());

            CreateMap<EditBudgetViewModel, Budget>()
                .ForMember(d => d.Categories, opt => opt.Ignore())
                .ForMember(d => d.Group, opt => opt.Ignore())
                .ForMember(d => d.Entries, opt => opt.Ignore())
                .ForMember(d => d.ConversionRatio, opt => opt.Ignore())
                .ForMember(d => d.StartDate, opt => opt.MapFrom(m => DateTime.ParseExact(m.StartDate, "MM/dd/yyyy", new CultureInfo("en-US"))))
                .ForMember(d => d.EndDate, opt => opt.MapFrom(m => DateTime.ParseExact(m.EndDate, "MM/dd/yyyy", new CultureInfo("en-US"))));
            CreateMap<Budget, EditBudgetViewModel>()
                .ForMember(d => d.CurrencyId, opt => opt.MapFrom(m => m.ConversionRatio.CurrencyFrom.Id))
                .ForMember(d => d.CurrenciesList, opt => opt.Ignore())
                .ForMember(d => d.CategoryIds, opt => opt.MapFrom(m => m.Categories.Select(x => x.Id)))
                .ForMember(d => d.CategoriesList, opt => opt.Ignore())
                .ForMember(d => d.GroupId, opt => opt.MapFrom(m => m.Group.Id))
                .ForMember(d => d.GroupsList, opt => opt.Ignore())
                .ForMember(d => d.StartDate, opt => opt.MapFrom(m => m.StartDate.ToString("MM/dd/yyyy")))
                .ForMember(d => d.EndDate, opt => opt.MapFrom(m => m.EndDate.ToString("MM/dd/yyyy")));


        }
    }
}
