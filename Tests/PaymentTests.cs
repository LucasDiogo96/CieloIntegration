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
    public class PaymentTests
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
        public void CreateCreditCardTransaction()
        {
            //Create Request object

            var transaction = new Transaction<CreditCard>()
            {
                OrderNumber = "2014111703",
                Amount = 17.50m,
                PaymentObject = new CreditCard()
                {
                    Holder = "Teste Holder",
                    CardNumber = "402400715376319",
                    ExpirationDate = "12/2021",
                    SecurityCode = "123",
                    Installments = 2
                },
                Customer = new Customer()
                {
                    Name = "Lucas Diogo da Silva",
                    Identity = "50835252086",
                    Birthdate = "1991-01-02",
                    Email = "compradorteste@teste.com",
                    Address = new Address()
                    {
                        Street = "Rua Teste",
                        Number = "123",
                        Complement = "AP 123",
                        ZipCode = "12345987",
                        District = "Santa Isabel",
                        City = "Rio de Janeiro",
                        State = "RJ",
                        Country = "BRA"
                    }
                }
            };

            IActionResult result = new CieloPaymentController(options).Create(transaction);


            var okResult = result as OkObjectResult;
            var responseController = okResult.Value as ResponseModel<TransactionResponseDetail>;

            var responseObject = (Transaction<CreditCard>)responseController.response.data.Detail;

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(responseObject.TransactionStatus, Status.Authorized);

        }

        [TestMethod]
        public void CreateDebitCardTransaction()
        {
            //Create Request object

            var transaction = new Transaction<DebitCard>()
            {
                OrderNumber = "2014111703",
                Amount = 17.50m,
                PaymentObject = new DebitCard()
                {
                    Holder = "Teste Holder",
                    CardNumber = "402400715376319",
                    ExpirationDate = "12/2030",
                    SecurityCode = "123"
                },
                Customer = new Customer()
                {
                    Name = "Lucas Diogo da Silva",
                    Identity = "50835252086",
                    Birthdate = "1991-01-02",
                    Email = "compradorteste@teste.com",
                    Address = new Address()
                    {
                        Street = "Rua Teste",
                        Number = "123",
                        Complement = "AP 123",
                        ZipCode = "12345987",
                        District = "Santa Isabel",
                        City = "Rio de Janeiro",
                        State = "RJ",
                        Country = "BRA"
                    }
                }
            };

            IActionResult result = new CieloPaymentController(options).Create(transaction);


            var okResult = result as OkObjectResult;
            var responseController = okResult.Value as ResponseModel<TransactionResponseDetail>;

            var responseObject = (Transaction<DebitCard>)responseController.response.data.Detail;

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(responseObject.TransactionStatus, Status.PaymentConfirmed);
        }


        [TestMethod]
        public void CreateBankslipTransaction()
        {
            //Create Request object

            var transaction = new Transaction<Bankslip>()
            {
                OrderNumber = "2014111703",
                Amount = 17.50m,
                PaymentObject = new Bankslip()
                {
                    Type = "Boleto",
                    Provider = "INCLUIR PROVIDER",
                    Address = "Rua Teste",
                    BoletoNumber = "123",
                    Assignor = "Empresa Teste",
                    Demonstrative = "Desmonstrative Teste",
                    ExpirationDate = "2020-12-31",
                    Identification = "11884926754",
                    Instructions = "Aceitar somente até a data de vencimento, após essa data juros de 1% dia."
                },
                Customer = new Customer()
                {
                    Name = "Lucas Diogo da Silva",
                    Identity = "50835252086",
                    Birthdate = "1991-01-02",
                    Email = "compradorteste@teste.com",
                    Address = new Address()
                    {
                        Street = "Rua Teste",
                        Number = "123",
                        Complement = "AP 123",
                        ZipCode = "12345987",
                        District = "Santa Isabel",
                        City = "Rio de Janeiro",
                        State = "RJ",
                        Country = "BRA"
                    }
                }
            };

            IActionResult result = new CieloPaymentController(options).Create(transaction);


            var okResult = result as OkObjectResult;
            var responseController = okResult.Value as ResponseModel<TransactionResponseDetail>;

            var responseObject = (Transaction<Bankslip>)responseController.response.data.Detail;

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(responseObject.TransactionStatus, Status.Authorized);
        }
    }
}
