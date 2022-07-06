using ExchangeRate_Domains.Models;
using ExchangeRate_Domains.Models.Request;
using ExchangeRate_Domains.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate_Application.Interface
{
    public interface IExchangeRateService
    {
        Task<ExchangeRateresponse> GetLatestExchangeRate(ExchangeRateRequest exchangeRateRequest, string UserId);
        Task<bool> insertExchangeRate( ExchangeRate exchangeRate);
        Task<bool> insertTrade(Trade trade);
        Task<Trade> GetTradeByIdAsync(Guid tradeId);
        Task<List<ExchangeRate>> GetAllExchangeRatebyUseridAsync(GetallExchangeRateFilter filter = null, PaginationFilter paginationFilter = null);
    
    }
}
