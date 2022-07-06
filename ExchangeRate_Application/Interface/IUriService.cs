using ExchangeRate_Domains.Models.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate_Application.Interface
{
    public interface IUriService
    {
        Uri GetAllExchangeRateUri(PaginationQuery pagination = null);
        Uri GetTradeUri(string tradeId);

    }
}
