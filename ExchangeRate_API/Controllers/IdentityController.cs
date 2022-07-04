using ExchangeRate_Application.Interface;
using ExchangeRate_Domains.Constants;
using ExchangeRate_Domains.Models.Request;
using ExchangeRate_Domains.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRate_API.Controllers
{

    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger<IdentityController> _logger;

        public IdentityController(ILogger<IdentityController> logger, IIdentityService identityService)
        {
            _logger = logger;
            _identityService = identityService;
        }

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            try
            {
                _logger.LogInformation("Calling Registration Endpoint................");
                if (!ModelState.IsValid)
                {
                    return BadRequest(new AuthFailedResponse
                    {
                        Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                    });
                }

                var authResponse = await _identityService.RegisterAsync(request.Email, request.Password);

                if (!authResponse.Success)
                {
                    return BadRequest(new AuthFailedResponse
                    {
                        Errors = authResponse.Errors
                    });
                }

                return Ok(new AuthSuccessResponse
                {
                    Token = authResponse.Token,
                    RefreshToken = authResponse.RefreshToken
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
           
        }

        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            try
            {
                var authResponse = await _identityService.LoginAsync(request.Email, request.Password);

                if (!authResponse.Success)
                {
                    return BadRequest(new AuthFailedResponse
                    {
                        Errors = authResponse.Errors
                    });
                }

                return Ok(new AuthSuccessResponse
                {
                    Token = authResponse.Token,
                    RefreshToken = authResponse.RefreshToken
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500,ex.Message);
            }
        
        }

        [HttpPost(ApiRoutes.Identity.Refresh)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var authResponse = await _identityService.RefreshTokenAsync(request.Token, request.RefreshToken);

                if (!authResponse.Success)
                {
                    return BadRequest(new AuthFailedResponse
                    {
                        Errors = authResponse.Errors
                    });
                }

                return Ok(new AuthSuccessResponse
                {
                    Token = authResponse.Token,
                    RefreshToken = authResponse.RefreshToken
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                 return StatusCode(500, ex.Message);
            }
        
        }

    }
}
