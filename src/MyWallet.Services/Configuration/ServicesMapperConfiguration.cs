using AutoMapper;
using MyWallet.Services.DataTransferModels.Mapping;

namespace MyWallet.Services.Configuration
{
    internal static class ServicesMapperConfiguration
    {
        public static void InitializeMappings(IMapperConfigurationExpression configuration)
        {
            configuration.AddProfile<BudgetsMappingProfile>();
            configuration.AddProfile<CategoryMapping>();
            configuration.AddProfile<GroupMapping>();
            configuration.AddProfile<UserMappingProfile>();
            configuration.AddProfile<CurrencyMappingProfile>();
            configuration.AddProfile<EntryMappingProfile>();
            configuration.AddProfile<ConversionRatioMappingProfile>();
        }
        
    }
}