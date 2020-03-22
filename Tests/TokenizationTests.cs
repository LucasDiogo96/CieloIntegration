using Domain.Entities;
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
    public class TokenizationTests
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

        private static ResponseModel<TokenizationResponse> SendRequest(string Json, string Endpoint)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:7000/api/");

                client.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", Token);

                var content = new StringContent(Json, Encoding.UTF8, "application/json");
                var result = client.PostAsync(Endpoint, content).Result;

                var detail = JsonConvert.DeserializeObject<ResponseModel<TokenizationResponse>>(result.Content.ReadAsStringAsync().Result);
                return detail;
            };
        }

        [TestInitialize]
        public void SetUp()
        {
            Token = Authenticate();
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

            var Json = JsonConvert.SerializeObject(credit);

            ResponseModel<TokenizationResponse> response = SendRequest(Json, "Tokenization/Create");

            var responseObject = (TokenizationResponse)response.response.data;
            Assert.IsNotNull(responseObject.CardToken);
        }
    }
}
