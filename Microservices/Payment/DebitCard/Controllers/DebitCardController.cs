using Domain.Entities;
using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Services;
using System;

namespace DebitCard.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DebitCardController : ControllerBase
    {

        protected internal Configuration _cieloConfiguration;
        private IPaymentService _paymentservice;

 
        public DebitCardController(IOptions<Configuration> cieloConfiguration)
        {
            _cieloConfiguration = cieloConfiguration.Value;
            _paymentservice = new PaymentService(_cieloConfiguration);
        }

        //[Authorize]
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody]Transaction<Domain.Entities.DebitCard> transaction)
        {
            try
            {

                TransactionResponseDetail result = _paymentservice.CreateTransactionDebitCard(transaction);

                return Ok(new ResponseModel<TransactionResponseDetail>
                {
                    response = new ResponseDataModel<TransactionResponseDetail>
                    {
                        data = result,
                        success = !result.HasError,
                        message = result.HasError ? "Não foi possível efetuar a cobrança!" : "Cobrança gerada com sucesso!"
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<TransactionResponseDetail>
                {
                    response = new ResponseDataModel<TransactionResponseDetail> { success = false, message = ex.Message }
                });
            }
        }
    }
}
