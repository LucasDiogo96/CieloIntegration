using Domain.Entities;
using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Services;
using System;

namespace Boleto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankslipController : ControllerBase
    {
        protected internal Configuration _cieloConfiguration;
        private IPaymentService _paymentservice;

        public BankslipController(IOptions<Configuration> cieloConfiguration)
        {
            _cieloConfiguration = cieloConfiguration.Value;
            _paymentservice = new PaymentService(_cieloConfiguration);
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody]Transaction<Bankslip> transaction)
        {
            try
            {

                TransactionResponseDetail result = _paymentservice.CreateTransactionBankslip(transaction);

                return Ok(new ResponseModel<TransactionResponseDetail>
                {
                    response = new ResponseDataModel<TransactionResponseDetail>
                    {
                        data = result,
                        success = !result.HasError,
                        message = result.HasError ? "Não foi possível efetuar a cobrança!" : "Boleto gerado com sucesso!"
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
