using Domain.Entities;
using System;

namespace Domain.Services.Interfaces
{
    public interface ITransactionService
    {
        public TransactionResponseDetail Cancel(Guid PaymentId);
        public TransactionResponseDetail Get(Guid PaymentId, bool Callback);
    }
}
