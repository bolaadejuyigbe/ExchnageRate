﻿using AutoMapper;
using ExchangeRate_API.Extension;
using ExchangeRate_Application.Interface;
using ExchangeRate_Domains.Constants;
using ExchangeRate_Domains.Models;
using ExchangeRate_Domains.Models.Query;
using ExchangeRate_Domains.Models.Request;
using ExchangeRate_Domains.Models.Response;
using ExchangeRate_API.Cache;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ExchangeRate_API.Helpers;

namespace ExchangeRate_API.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    
    public class ExchangeRateController : ControllerBase
    {
        private readonly IExchangeRateService _exchangeRateService;
        private ILogger<ExchangeRateController> _logger;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
    
        public ExchangeRateController(IExchangeRateService exchangeRateService, ILogger<ExchangeRateController> logger, IMapper mapper, IUriService uriService)
        {
            _exchangeRateService = exchangeRateService;
            _logger = logger;
            _mapper = mapper;
            _uriService = uriService;
    
        }

        [HttpGet(ApiRoutes.ExchangeRate.GetLatestRate)]
    
        public async Task<IActionResult> GetLatestRate([FromHeader] string baseCurrency, string symbol)
        {
            try
            {
                if (baseCurrency == null || symbol == null)
                {
                    return BadRequest(new ErrorResponse(new ErrorModel { Message = "Basecurrency or Symbol Cannot be null" }));
                }
                var UserId = HttpContext.GetUserId();
                ExchangeRateRequest exchangeRateRequest = new ExchangeRateRequest
                {
                    Base = baseCurrency,
                    Symbol = symbol,
                };

                var response = await _exchangeRateService.GetLatestExchangeRate(exchangeRateRequest, UserId);
             
        
                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                    return Created(baseUrl, response);
              
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

       [HttpGet(ApiRoutes.ExchangeRate.GetUsedExchangeRate)]
        [Cached(120)]
        public async Task<IActionResult> GetUsedExchangeRate([FromQuery] GetAllExchangeRateQuery query, [FromQuery] PaginationQuery paginationQuery)
        {
            try
            {
            
                var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
                var filter = _mapper.Map<GetallExchangeRateFilter>(query);
                var rates = await _exchangeRateService.GetAllExchangeRatebyUseridAsync(filter, pagination);
                var ratesResponse = _mapper.Map<List<ExchangeRate>>(rates);

                if (pagination == null || pagination.PageNumber < 1 || pagination.PageSize < 1)
                {
                    return Ok(new PagedResponse<ExchangeRate>(ratesResponse));
                }

                var paginationResponse = PaginationHelpers.CreatePaginatedResponse(_uriService, pagination, ratesResponse);
                return Ok(paginationResponse);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);

            }
           
        }
    }
}
