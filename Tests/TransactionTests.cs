using Domain;
using Domain.Entities;
using Domain.Enums;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Tests
{
    [TestClass]
    public class TransactionTests
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

        private static ResponseModel<TransactionResponseDetail> SendRequest(string IdTransaction, string Endpoint, bool IsCancelRequest = false)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:7000/api/");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                if (!IsCancelRequest)
                {
                    var result = client.GetAsync(Endpoint + "?Idtransaction=" + IdTransaction).Result;

                    var detail = JsonConvert.DeserializeObject<ResponseModel<TransactionResponseDetail>>(result.Content.ReadAsStringAsync().Result);
                    return detail;
                }
                else
                {
                    var result = client.DeleteAsync(Endpoint + "?Idtransaction=" + IdTransaction).Result;

                    var detail = JsonConvert.DeserializeObject<ResponseModel<TransactionResponseDetail>>(result.Content.ReadAsStringAsync().Result);
                    return detail;
                }            
            };
        }

        [TestInitialize]
        public void SetUp()
        {
            Token = Authenticate();
        }

        [TestMethod]
        public void Get()
        {

            const string RoutePath = "Transaction/Get";

            ///GET BANKSLIP TRANSACTION
            ResponseModel<TransactionResponseDetail> responseBankslip = SendRequest("2043ce07-4c5c-4a70-8a99-95875eec4ed0", RoutePath);

            var Bankslip =  JsonConvert.DeserializeObject<BankslipPaymentResponse>(responseBankslip.response.data.Detail.ToString());

            Assert.AreEqual(Bankslip.Status, (int)Status.Authorized);


            ///GET CREDIT CARD TRANSACTION
            ResponseModel<TransactionResponseDetail> responseCreditCard = SendRequest("06363b01-ef50-4c58-923f-8330e800a646", RoutePath);

            var CreditCard = JsonConvert.DeserializeObject<CreditCardPaymentResponse>(responseCreditCard.response.data.Detail.ToString());

            Assert.AreEqual(CreditCard.Status, (int)Status.PaymentConfirmed);


            ///GET DEBIT CARD TRANSACTION
            ResponseModel<TransactionResponseDetail> responseDebit = SendRequest("733a3dc1-aec7-492b-9fb1-398056110afe", RoutePath);

            var DebitCard = JsonConvert.DeserializeObject<CreditCardPaymentResponse>(responseCreditCard.response.data.Detail.ToString());

            Assert.AreEqual(DebitCard.Status,(int) Status.Authorized);
        }

        [TestMethod]
        public void Cancel()
        {

            ResponseModel<TransactionResponseDetail> response = SendRequest("2043ce07-4c5c-4a70-8a99-95875eec4ed0", "Transaction/Cancel",true);

            var responseObject = response.response.data.Detail;

        }
    }
}
