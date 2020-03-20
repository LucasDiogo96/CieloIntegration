using Domain.Entities;
using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Service;
using System;

namespace CieloServices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        protected internal Configuration _cieloConfiguration;
        private ITransactionService _service;


        public TransactionController(IOptions<Configuration> cieloConfiguration)
        {
            _cieloConfiguration = cieloConfiguration.Value;
            _service = new TransactionService(_cieloConfiguration);
        }

        //[Authorize]
        [HttpGet]
        [Route("Get")]
        public IActionResult Get([FromQuery]Guid Idtransaction)
        {
            try
            {
                TransactionResponseDetail response = _service.Get(Idtransaction, false);

                return Ok(new ResponseModel<TransactionResponseDetail>
                {
                    response = new ResponseDataModel<TransactionResponseDetail>
                    {
                        data = response,
                        success = true,
                        message = "Dados buscados com sucesso!"
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

        [Authorize]
        [HttpDelete]
        [Route("Cancel")]
        public IActionResult Cancel([FromQuery]Guid Idtransaction)
        {
            try
            {
                TransactionResponseDetail response = _service.Cancel(Idtransaction);

                return Ok(new ResponseModel<TransactionResponseDetail>
                {
                    response = new ResponseDataModel<TransactionResponseDetail>
                    {
                        data = response,
                        success = true,
                        message = "Transação cancelada com sucesso!"
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