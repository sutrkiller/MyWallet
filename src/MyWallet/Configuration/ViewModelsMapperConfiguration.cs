using AutoMapper;
using MyWallet.Models;
using MyWallet.Models.Mappings;

namespace MyWallet.Configuration
{
    public class ViewModelsMapperConfiguration
    {
        public static void InitializeMappings(IMapperConfigurationExpression cfg)
        {
            cfg.AddProfile<BudgetsMappingProfile>();
            cfg.AddProfile<EntriesMappingProfile>();
            cfg.AddProfile<CategoriesMappingProfile>();
        }
    }
}
