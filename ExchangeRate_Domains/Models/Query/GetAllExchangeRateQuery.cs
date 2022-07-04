using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate_Domains.Models.Query
{
    public class GetAllExchangeRateQuery
    {
        [FromQuery(Name = "userId")]
        public string UserId { get; set; }
    }
}
