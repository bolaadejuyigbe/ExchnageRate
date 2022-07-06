using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRate_Domains.Models.Request
{
    public class TradeRequest
    {
        public string BaseCurrency { get; set; }
        public string TargetCurrency { get; set; }
      
        public decimal Amount { get; set; }
    }
}
