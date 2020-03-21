﻿
using Domain.Entities;
using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Services;
using System;


namespace CreditCard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreditCardController : ControllerBase
    {
        protected internal Configuration _cieloConfiguration;
        private IPaymentService _paymentservice;

        public CreditCardController(IOptions<Configuration> cieloConfiguration)
        {
            _cieloConfiguration = cieloConfiguration.Value;
            _paymentservice = new PaymentService(_cieloConfiguration);
        }

        //[Authorize]
        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody] Transaction<Domain.Entities.CreditCard> transaction)
        {
            try
            {
                TransactionResponseDetail result = _paymentservice.CreateTransactionCreditCard(transaction);

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
