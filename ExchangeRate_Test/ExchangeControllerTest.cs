using ExchangeRate_Domains.Constants;
using ExchangeRate_Domains.Models;
using ExchangeRate_Domains.Models.Request;
using ExchangeRate_Domains.Models.Response;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExchangeRate_Test
{
    public class ExchangeControllerTest : IntegrationTest
    {
        [Fact]
        public async Task GetAllUsedRate_WithoutAnyExchangeRate_ReturnsEmptyResponse()
        {
            //Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync(ApiRoutes.ExchangeRate.GetUsedExchangeRate);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<PagedResponse<ExchangeRateresponse>>()).Data.Should().BeEmpty();
        }

        [Fact]
        public async Task Get_ReturnsPost_WhenPostExistsInTheDatabase()
        {
            // Arrange
            await AuthenticateAsync();
            var createdTrade = await CreateExchangeRateAsync (new TradeRequest
            {

                TargetCurrency = "EUR",
                BaseCurrency = "USD",
                Amount = 5
               
            });

            // Act
            var response = await TestClient.GetAsync(ApiRoutes.ExchangeRate.Get.Replace("{tradeId}", createdTrade.Id.ToString()));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedPost = await response.Content.ReadAsAsync<Response<TradeResponse>>();
            returnedPost.Data.Id.Should().Be(createdTrade.Id);

            }
        }
}
