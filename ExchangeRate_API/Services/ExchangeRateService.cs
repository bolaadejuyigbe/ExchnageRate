using ExchangeRate_Application.Interface;
using ExchangeRate_Domains.Models;
using ExchangeRate_Domains.Models.Request;
using ExchangeRate_Domains.Models.Response;
using ExchangeRate_API.Data;
using Microsoft.EntityFrameworkCore;
using ExchangeRate_API.Extension;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate_Infrastructure.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _config;
    

        public ExchangeRateService(DataContext dataContext, IConfiguration config)
        {
            _dataContext = dataContext;
            _config = config;   
        
        }

        public async Task<List<ExchangeRate>> GetAllExchangeRatebyUseridAsync(GetallExchangeRateFilter filter = null, PaginationFilter paginationFilter = null)
        {
            try
            {
                var queryable = _dataContext.ExchangeRates.AsQueryable();
                queryable = AddFiltersOnQuery(filter, queryable);

                var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
                var res = await queryable
                    .Skip(skip).Take(paginationFilter.PageSize).ToListAsync();
               
                return res;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<ExchangeRateresponse> GetLatestExchangeRate(ExchangeRateRequest exchangeRateRequest, string UserId)
        {
        
                try
                {
                    var client = new RestClient();
                    var resource = _config.GetValue<string>("ExchangeRate_Url");
                    var apiKey = _config.GetValue<string>("ExchangeRate_APIKey");

                    var request = new RestRequest(resource, Method.Get);
                    request.AddHeader("apikey", apiKey);
                   request.AddParameter("base",exchangeRateRequest.Base);
                   request.AddParameter("symbols",exchangeRateRequest.Symbol);
                    var response = await client.ExecuteAsync(request);
                    switch (response.StatusCode)
                    {
                        case System.Net.HttpStatusCode.OK:
                            var settings = new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore,
                                MissingMemberHandling = MissingMemberHandling.Ignore
                            };
                       var resp = JsonConvert.DeserializeObject<ExchangeRateresponse>(response.Content, settings);
                        var newRateId = Guid.NewGuid();
                        ExchangeRate exchangeRate = new ExchangeRate
                        {
                            Base = resp._base,
                            Currency = exchangeRateRequest.Symbol,
                            date = resp.date,
                            success = true,
                            Symbol = exchangeRateRequest.Symbol,
                            DateProcessed = DateTime.Now,
                            Rates = resp.ConversionRates.Values.FirstOrDefault(),
                            timestamp = resp.timestamp,
                            UserId = UserId,
                            Id = newRateId

                        };
                        var insert = await insertExchangeRate(exchangeRate);
                            return resp;

                        case System.Net.HttpStatusCode.BadRequest:
                            var err = JsonConvert.DeserializeObject<ExchangeRateresponse>(response.Content);
                        return err;
                        case System.Net.HttpStatusCode.InternalServerError:
                        case System.Net.HttpStatusCode.Unauthorized:
                        default:
                        var defaultError = JsonConvert.DeserializeObject<Result>(response.Content);
                        ExchangeRateresponse rateresponse = new ExchangeRateresponse
                        {
                            IsSuccess = false,
                            timestamp = 0,
                            ConversionRates = null,
                            _base = null,
                            Error = defaultError.Error,
                           
                        };
                        return rateresponse;

                }
                }
                catch (Exception)
                {
                    throw;
                }

         }
            
        

        private static IQueryable<ExchangeRate> AddFiltersOnQuery(GetallExchangeRateFilter filter, IQueryable<ExchangeRate> queryable)
        {
            if (!string.IsNullOrEmpty(filter?.UserId))
            {
                queryable = queryable.Where(x => x.UserId == filter.UserId && x.DateProcessed >= DateTime.Now.AddMinutes(-60));
            }

            return queryable;
        }

        public async Task<bool> insertExchangeRate(ExchangeRate exchangeRate)
        {
            try
            {
              
                await _dataContext.ExchangeRates.AddAsync(exchangeRate);

                var created = await _dataContext.SaveChangesAsync();
                return created > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
