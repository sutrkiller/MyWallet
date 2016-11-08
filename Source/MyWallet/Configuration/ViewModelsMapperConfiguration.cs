using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyWallet.Services.DataTransferModels.Mapping;

namespace MyWallet.Configuration
{
    public class ViewModelsMapperConfiguration
    {
        public static void InitialializeMappings(IMapperConfigurationExpression cfg)
        {
            cfg.AddProfile<BudgetMappingProfile>();
        }
    }
}
