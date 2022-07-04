using ExchangeRate_API;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RestSharp;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ExchangeRate_Domains.Constants;
using ExchangeRate_Domains.Models.Request;
using System.Net.Http.Headers;
using ExchangeRate_Domains.Models.Response;
using ExchangeRate_Infrastructure;
using ExchangeRate_API.Data;

namespace ExchangeRate_Test
{
    public class IntegrationTest :IDisposable
    {
        protected readonly HttpClient TestClient;
       
        private readonly IServiceProvider _serviceProvider;
    
        protected IntegrationTest()
        {
          
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(DataContext));
                        services.AddDbContext<DataContext>(options =>
                        {
                            options.UseInMemoryDatabase(databaseName: "Test");
                           

                        });
                    });

                });

            _serviceProvider = appFactory.Services;
            TestClient = appFactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        protected async Task<ExchangeRateresponse> GetLatestExchangeRateAsync(ExchangeRateRequest request)
        {
            var response = await TestClient.GetAsync(ApiRoutes.ExchangeRate.GetLatestRate + $"?base={request.Base}&symbols={request.Symbol}");

            return (await response.Content.ReadAsAsync<Response<ExchangeRateresponse>>()).Data;
        }

        private async Task<string> GetJwtAsync()
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Identity.Register, new UserRegistrationRequest
            {
                Email = "test@integration.com",
                Password = "SomePass1234!"
            });

            var registrationResponse = await response.Content.ReadAsAsync<AuthSuccessResponse>();
            return registrationResponse.Token;
        }

        public void Dispose()
        {
            using var serviceScope = _serviceProvider.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<DataContext>();
            context.Database.EnsureDeleted();

        }
    }
}
