using Domain;
using Domain.Entities;
using Domain.Services.Interfaces;
using Lib.Exceptions;
using Lib.Extensions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Net;

namespace Services
{
    public class CieloService : ICieloService
    {

        private string resourceUrl = "1/sales/";

        protected internal Configuration _cieloConfiguration;

        public CieloService(IOptions<Configuration> cieloConfiguration)
        {
            _cieloConfiguration = cieloConfiguration.Value;
        }

        public CieloService(Configuration cieloConfiguration)
        {
            _cieloConfiguration = cieloConfiguration;
        }

        private RestClient client = new RestClient();


        #region methods

        public CreditCardResponse CreateCreditTransaction(CreditCardRequest request)
        {
            CieloRequest param = new CieloRequest()
            {
                baseUrl = _cieloConfiguration.DefaultEndpoint,
                method = Method.POST,
                resource = resourceUrl,
                body = request
            };
            return Execute<CreditCardResponse>(param);
        }

        public BankslipResponse CreateBankslipTransaction(BankslipRequest request)
        {
            CieloRequest param = new CieloRequest()
            {
                baseUrl = _cieloConfiguration.DefaultEndpoint,
                method = Method.POST,
                resource = resourceUrl,
                body = request
            };
            return Execute<BankslipResponse>(param);
        }

        public QrCodeResponse CreateQrCodeTransaction(QrCodeRequest request)
        {
            CieloRequest param = new CieloRequest()
            {
                baseUrl = _cieloConfiguration.DefaultEndpoint,
                method = Method.POST,
                resource = resourceUrl,
                body = request
            };
            return Execute<QrCodeResponse>(param);
        }

        public CieloResponse CancelTransaction(Guid? paymentId = null, string merchantOrderId = null, decimal amount = 0)
        {
            if (paymentId == null && String.IsNullOrWhiteSpace(merchantOrderId))
                throw new ArgumentNullException("In order to proceed with the cancellation, you must provide the 'paymentId' or 'merchantOrderId'");

            CieloRequest param = new CieloRequest()
            {
                baseUrl = _cieloConfiguration.DefaultEndpoint,
                method = Method.PUT,
                resource = (amount == 0 ?
                (paymentId != null ? $"/{resourceUrl}{paymentId}/void" : $"/{resourceUrl}OrderId/{merchantOrderId}/void") :
                (paymentId != null ? $"/{resourceUrl}{paymentId}/void?amount={amount}" : $"/{resourceUrl}OrderId/{merchantOrderId}/void?amount={amount}"))
            };

            return Execute<CieloResponse>(param);
        }

        public CieloResponse CaptureTransaction(Guid paymentId, decimal amount = 0)
        {
            int partialCapture = (int)(amount * 100);
            CieloRequest param = new CieloRequest()
            {
                baseUrl = _cieloConfiguration.DefaultEndpoint,
                method = Method.PUT,
                resource = $"/{resourceUrl}{paymentId}/capture" + (partialCapture > 0 ? $"?amount={partialCapture}" : "")
            };

            return Execute<CieloResponse>(param);
        }

        public TransactionResponse<JObject> GetTransaction<T>(Guid? paymentId = null, string merchantOrderId = null)
        {
            if (paymentId == null && String.IsNullOrWhiteSpace(merchantOrderId))
                throw new ArgumentNullException("In order to proceed with the cancellation, you must provide the 'paymentId' or 'merchantOrderId'");

            CieloRequest param = new CieloRequest()
            {
                baseUrl = _cieloConfiguration.QueryEndpoint,
                method = Method.GET,
                resource = (paymentId != null ? $"/{resourceUrl}{paymentId}" : $"/1/sales?merchantOrderId={merchantOrderId}")
            };

            return Execute<TransactionResponse<JObject>>(param);
        }

        public T Execute<T>(CieloRequest requestParam) where T : class
        {
            try
            {
                client.BaseUrl = new Uri(requestParam.baseUrl);
                var request = new RestRequest(requestParam.resource.CleanPathUrl(requestParam.baseUrl), requestParam.method);

                ConfigureExecute(requestParam, ref request);

                var response = client.Execute(request);

                if ((response.StatusCode != HttpStatusCode.Created) &&
                    (response.StatusCode != HttpStatusCode.OK))
                {
                    throw new ResponseException(new ErrorResponse(response.Content, response.StatusCode));
                }

                return JsonConvert.DeserializeObject<T>(response.Content);
            }
            catch (ResponseException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ConfigureExecute(CieloRequest requestParam, ref RestRequest request)
        {
            #region parameters

            if (requestParam.param != null)
            {
                foreach (Parameter p in requestParam.param)
                {
                    request.AddParameter(p);
                }
            }
            #endregion adding parameters

            #region authentication methods

            request.AddParameter("MerchantId", _cieloConfiguration.MerchantId, ParameterType.HttpHeader);
            request.AddParameter("MerchantKey", _cieloConfiguration.MerchantKey, ParameterType.HttpHeader);
            request.AddParameter("RequestId", Guid.NewGuid(), ParameterType.HttpHeader);

            #endregion authentication

            #region header request params

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;

            #endregion

            if (requestParam.body != null)

                request.AddParameter("application/json",
                       JsonConvert.SerializeObject(requestParam.body, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                       ParameterType.RequestBody);
        }
        #endregion
    }
}
