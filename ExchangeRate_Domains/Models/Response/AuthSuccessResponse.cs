using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate_Domains.Models.Response
{
    public class AuthSuccessResponse
    {

        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}