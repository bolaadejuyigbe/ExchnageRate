using ExchangeRate_Domains.Constants;
using ExchangeRate_Domains.Models;
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
    }
}
