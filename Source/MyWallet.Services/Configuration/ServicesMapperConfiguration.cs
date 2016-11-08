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
            configuration.AddProfile<BudgetsMappingProfile>();

        }
    }
}
