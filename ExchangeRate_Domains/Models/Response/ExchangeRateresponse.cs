using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate_Domains.Models.Response
{
    public class ExchangeRateresponse : Result
    {

   
         
            public int timestamp { get; set; }
        [JsonProperty("base")]
        public string _base { get; set; }
            public string date { get; set; }
            [JsonProperty("rates")]
     
          public Dictionary<string, decimal>? ConversionRates { get; set; }

    }
}
