using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRate_Domains.Models.Response
{
    public class TradeResponse
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public decimal ConversionRate { get; set; }  
    }
}
