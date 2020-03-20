using Domain.Entities;
using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Solar.Services.Services;
using System;

namespace CieloServices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenizationController : ControllerBase
    {
        protected internal Configuration _cieloConfiguration;
        private ITokenizationService _tokenizationservice;

        public TokenizationController(IOptions<Configuration> cieloConfiguration)
        {
            _cieloConfiguration = cieloConfiguration.Value;
            _tokenizationservice = new TokenizationService(_cieloConfiguration);
        }

        [Authorize]
        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody]CreditCard creditCard)
        {
            try
            {

                TokenizationResponse response = _tokenizationservice.Create(creditCard);

                return Ok(new ResponseModel<TokenizationResponse>
                {
                    response = new ResponseDataModel<TokenizationResponse>
                    {
                        data = response,
                        success = true,
                        message = "Cartão tokenizado com sucesso!"
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<TokenizationResponse>
                {
                    response = new ResponseDataModel<TokenizationResponse> { success = false, message = ex.Message }
                });
            }
        }
    }
}