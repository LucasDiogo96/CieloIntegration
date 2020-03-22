using Domain.Entities;
using Domain.Enums;
using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Tests
{
    [TestClass]
    public class PaymentTests
    {
        private static string Token = null;
        private static string Authenticate()
        {
            var Auth = new
            {
                AppName = "CieloApplication",
                AppKey = "3&uXZ0IjYCoOnG3rQK3SCLw^HPF63YyV1nBKHXj1C&OldaFD@S"
            };
            var jsonObject = JsonConvert.SerializeObject(Auth);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:7000");

                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                var result = client.PostAsync("/api/Authentication/Authenticate", content).Result;

                dynamic jwt = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result);

                return jwt.access_token;
            };
        }

        private static ResponseModel<TransactionResponseDetail> SendRequest(string Json, string Endpoint)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:7000/api/");

                client.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", Token);

                var content = new StringContent(Json, Encoding.UTF8, "application/json");
                var result = client.PostAsync(Endpoint, content).Result;

                var detail = JsonConvert.DeserializeObject<ResponseModel<TransactionResponseDetail>>(result.Content.ReadAsStringAsync().Result);
                return detail;
            };
        }


        [TestInitialize]
        public void SetUp()
        {
            Token = Authenticate();
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
                    CardNumber = "1234123412341231",
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

            var Json = JsonConvert.SerializeObject(transaction);

            ResponseModel<TransactionResponseDetail> response = SendRequest(Json, "CreditCard/Create");

            var responseObject = JsonConvert.DeserializeObject<Transaction<CreditCard>>(response.response.data.Detail.ToString());
                
            Assert.AreEqual(responseObject.TransactionStatus, Status.PaymentConfirmed);

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

            var Json = JsonConvert.SerializeObject(transaction);

            ResponseModel<TransactionResponseDetail> response = SendRequest(Json, "DebitCard/Create");

            var responseObject = JsonConvert.DeserializeObject<Transaction<DebitCard>>(response.response.data.Detail.ToString());

            Assert.AreEqual(responseObject.TransactionStatus, Status.NotFinished);
            Assert.IsNotNull(responseObject.PaymentObject.AuthenticationUrl);
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

            var Json = JsonConvert.SerializeObject(transaction);

            ResponseModel<TransactionResponseDetail> response = SendRequest(Json, "Bankslip/Create");

            var responseObject = JsonConvert.DeserializeObject<Transaction<Bankslip>>(response.response.data.Detail.ToString());

            Assert.AreEqual(responseObject.TransactionStatus, Status.Authorized);
        }
    }
}
