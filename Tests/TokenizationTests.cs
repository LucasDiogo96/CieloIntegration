using CieloServices.API.Controllers;
using Domain.Entities;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Tests
{
    [TestClass]
    public class TokenizationTests
    {
        protected internal Configuration _cieloConfiguration;
        IOptions<Configuration> options = null;

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Test.json")
                .Build();
            return config;
        }

        [TestInitialize]
        public void SetUp()
        {
            var config = InitConfiguration();
            _cieloConfiguration = config.GetSection("Cielo").Get<Configuration>();
            options = Options.Create(_cieloConfiguration);
        }


        [TestMethod]
        public void Tokenize()
        {
            //Create Request object


            var credit = new CreditCard()
            {
                Holder = "Teste Holder",
                CardNumber = "5230510845570935",
                ExpirationDate = "12/2021",
                SecurityCode = "123",
                CustomerName = "Teste Holder"
            };

            IActionResult result = new TokenizationController(options).Create(credit);


            var okResult = result as OkObjectResult;
            var responseController = okResult.Value as ResponseModel<TokenizationResponse>;

            var responseObject = (TokenizationResponse)responseController.response.data;

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(responseObject.CardToken);

        }
    }
}
