using Domain;
using Domain.Entities;
using Domain.Enums;
using Domain.Services.Interfaces;
using Lib.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services;
using System;

namespace Service
{
    public class TransactionService : ITransactionService
    {
        protected internal Configuration _cieloConfiguration;
        protected internal ICieloService _cieloService;

        public TransactionService(Configuration cieloConfiguration)
        {
            _cieloConfiguration = cieloConfiguration;
            _cieloService = new CieloService(cieloConfiguration);
        }

        public TransactionResponseDetail Cancel(Guid PaymentId)
        {
            TransactionResponseDetail TResponse = new TransactionResponseDetail();

            try
            {
                CieloResponse response = _cieloService.CancelTransaction(PaymentId);

                TResponse.Detail = new
                {
                    Status = response.Status,
                    Description = response.ReturnMessage

                };
                TResponse.HasError = false;
            }
            catch (Exception e)
            {

                TResponse.Detail = new
                {
                    Status = 999,
                    Description = "Ocorreu um erro ao processar o cancelamento.",
                    Error = e.Message

                };
                TResponse.HasError = true;
            }

            return TResponse;
        }

        public TransactionResponseDetail Get(Guid PaymentId, bool Callback)
        {
            TransactionResponseDetail TResponse = new TransactionResponseDetail();

            try
            {
                var response = _cieloService.GetTransaction<TransactionResponse<JObject>>(PaymentId);

                if (response.Payment != null && response.Payment is JObject)
                {
                    if (response.Payment.ContainsKey("Type"))
                    {
                        PaymentType type = EnumExtensions.ToEnum<PaymentType>(response.Payment["Type"].ToString());

                        switch (type)
                        {
                            case PaymentType.CreditCard:
                                TResponse.Detail = JsonConvert.DeserializeObject<CreditCardPaymentRequest>(response.Payment.ToString());
                                break;
                            case PaymentType.Boleto:
                                TResponse.Detail = JsonConvert.DeserializeObject<BankslipPaymentRequest>(response.Payment.ToString());
                                break;
                            default:
                                throw new InvalidCastException("Não foi possível identificar o tipo do pagamento.");
                        }
                    }
                    else
                        throw new ArgumentNullException("TransactionType");
                }
                else
                    throw new ArgumentNullException("Payment");

                if (!Callback)
                {
                    TResponse.HasError = false;
                }
                else
                {
                    var status = TResponse.Detail?.GetType().GetProperty("Status")?.GetValue(TResponse.Detail, null);

                    TResponse.Detail = new
                    {
                        OrderNumber = response.MerchantOrderId,
                        PaymentId = PaymentId,
                        Status = status

                    };
                    TResponse.HasError = false;
                }
            }
            catch (Exception e)
            {

                TResponse.Detail = new
                {
                    Status = 999,
                    Description = "Ocorreu um erro ao buscar os dados da transação.",
                    Error = e.Message

                };
                TResponse.HasError = true;
            }

            return TResponse;
        }
    }
}