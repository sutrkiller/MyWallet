using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyWallet.Models.Mappings;


namespace MyWallet.Configuration
{
    public class ViewModelsMapperConfiguration
    {
        public static void InitialializeMappings(IMapperConfigurationExpression cfg)
        {
            cfg.AddProfile<BudgetsMappingProfile>();
        }
    }
}
