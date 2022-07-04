using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExchangeRate_Domains.Models
{
    public class Result
    {

        [JsonProperty("success")]
        public bool IsSuccess { get; set; }
        [JsonProperty("error")]
        public ResultError? Error { get; set; }
    }

 


    public class ResultError
    {
        [JsonProperty("code")]
        public int code { get; set; }
        [JsonProperty("message")]
        public string message{ get; set; }
    }
}
