using AutoMapper;
using ExchangeRate_Domains.Models;
using ExchangeRate_Domains.Models.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate_API.MappingProfiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<PaginationQuery, PaginationFilter>();
            CreateMap<GetAllExchangeRateQuery, GetallExchangeRateFilter>();
        }
    }
}
