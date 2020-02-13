using System;

namespace Solar.Domain.Interfaces.Services
{
    public interface ICieloService
    {
        CheckTransaction<JObject> GetTransaction<T>(Guid? paymentId = null, string merchantOrderId = null);
        CreditCardResponse CreateTransaction(TransactionRequest request);
        BankslipResponse CreateBankslip(BankslipRequest request);
        CieloResponse CancelTransaction(Guid? paymentId = null, string merchantOrderId = null, decimal amount = 0);
        CieloResponse CaptureTransaction(Guid paymentId, decimal amount = 0);
        public T Execute<T>(RequestParams requestParam) where T : class;
    }
}
