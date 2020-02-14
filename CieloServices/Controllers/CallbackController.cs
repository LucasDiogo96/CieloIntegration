using Domain.Entities;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Service;
using System;

namespace CieloServices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallbackController : Controller
    {
        protected internal Configuration _cieloConfiguration;

        public CallbackController(IOptions<Configuration> cieloConfiguration)
        {
            _cieloConfiguration = cieloConfiguration.Value;
        }

        [HttpPost]
        [Route("Cielo")]
        public IActionResult CieloCallback([FromBody]Callback callback)
        {
            try
            {
                ITransactionService service = new TransactionService(_cieloConfiguration);
                var response = service.Get(new Guid(callback.PaymentId), true);

                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}