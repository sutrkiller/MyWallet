using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyWallet.Services.DataTransferModels.Mapping;

namespace MyWallet.Services.Configuration
{
    public static class ServicesMapperConfiguration
    {
        public static void InitialializeMappings(IMapperConfigurationExpression configuration)
        {
            configuration.AddProfile<TimePeriodMappingProfile>();
            configuration.AddProfile<BudgetsMappingProfile>();
            configuration.AddProfile<GroupMappingProfile>();
            configuration.AddProfile<CategoryMappingProfile>();
            configuration.AddProfile<CurrencyMappingProfile>();
        }
    }
}
