using AutoMapper;
using ExchangeRate_Domains.Models;
using ExchangeRate_Domains.Models.Response;

namespace ExchangeRate_API.MappingProfiles
{
    public class DomainToResponseProfile:Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Trade, TradeResponse>();
        }
       
    }
}
