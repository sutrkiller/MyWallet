using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyWallet.Entities.Models;

namespace MyWallet.Services.DataTransferModels.Mapping
{
    internal class GroupMapping : Profile
    {
        public GroupMapping()
        {
            CreateMap<Entities.Models.Group, Group>()
                .ReverseMap();
        }
    }
}
