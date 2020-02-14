using Domain.Entities;
using Newtonsoft.Json.Linq;
using System;

namespace Domain.Services.Interfaces
{
    public interface ICieloService
    {
        TransactionResponse<JObject> GetTransaction<T>(Guid? paymentId = null, string merchantOrderId = null);
        CreditCardResponse CreateCreditTransaction(CreditCardRequest request);
        BankslipResponse CreateBankslipTransaction(BankslipRequest request);
        QrCodeResponse CreateQrCodeTransaction(QrCodeRequest request);
        CieloResponse CancelTransaction(Guid? paymentId = null, string merchantOrderId = null, decimal amount = 0);
        CieloResponse CaptureTransaction(Guid paymentId, decimal amount = 0);
        public T Execute<T>(CieloRequest requestParam) where T : class;
    }
}
