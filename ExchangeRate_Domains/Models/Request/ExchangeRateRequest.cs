using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate_Domains.Models.Request
{
    public class ExchangeRateRequest
    {
        [Required]
      
        public string Base {get; set;}
        [Required]
     
        public string Symbol {get; set;}
    }
}
