using Domain.Entities.BaseEntities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Service;

namespace Authentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        protected internal Audience _audience;
        private IAuthenticationService _authenticationservice;

        public AuthenticationController(IOptions<Audience> Audience)
        {
            _audience = Audience.Value;
            _authenticationservice = new AuthenticationService(_audience);
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticatioModel model)
        {
            var Auth = _authenticationservice.Authenticate(model.AppName, model.AppKey);

            if (Auth == null)
                return Unauthorized();

            return Ok(Auth);
        }
    }
}
