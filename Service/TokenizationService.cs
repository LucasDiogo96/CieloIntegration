using Domain.Entities;
using Domain.Services.Interfaces;
using RestSharp;
using Services;

namespace Services
{
    public class TokenizationService : ITokenizationService
    {
        protected internal Configuration _cieloConfiguration;
        ICieloService _cieloService;

        public TokenizationService(Configuration cieloConfiguration)
        {
            _cieloConfiguration = cieloConfiguration;
            _cieloService = new CieloService(cieloConfiguration);
        }


        public TokenizationResponse Create(CreditCard card)
        {
            CieloRequest param = new CieloRequest()
            {
                baseUrl = _cieloConfiguration.DefaultEndpoint,
                method = Method.POST,
                resource = "/1/card/",
                body = card
            };

            return _cieloService.Execute<TokenizationResponse>(param);
        }
    }
}
