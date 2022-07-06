using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate_Domains.Constants
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;
        public static class Identity
        {
            public const string Login = Base + "/identity/login";

            public const string Register = Base + "/identity/register";

            public const string Refresh = Base + "/identity/refresh";
        }

        public static class ExchangeRate
        {
            public const string GetLatestRate = Base + "/exchangerate/getlatestrate";
            public const string GetUsedExchangeRate = Base + "/exchangerate/getusedexchangerate";
            public const string ExchangeRateTrade = Base + "/exchangerate/exchangeratetrade";
            public const string Get = Base + "/exchangerate/{tradeId}";
    
        }
    }
}
