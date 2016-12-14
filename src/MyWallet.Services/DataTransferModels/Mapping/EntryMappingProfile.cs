using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyWallet.Entities.Models;

namespace MyWallet.Services.DataTransferModels.Mapping
{
    internal class EntryMappingProfile : Profile
    {
        public EntryMappingProfile()
        {
            CreateMap<Entities.Models.Entry, Entry>()
                .ReverseMap();

        }
    }
}
