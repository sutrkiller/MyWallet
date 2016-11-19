using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyWallet.Entities.Models;

namespace MyWallet.Services.DataTransferModels.Mapping
{
    public class BudgetsMappingProfile : Profile
    {
        public BudgetsMappingProfile()
        {
            CreateMap<Budget, BudgetDTO>()
                .MaxDepth(1)
                .ReverseMap();
        }
    }
}
