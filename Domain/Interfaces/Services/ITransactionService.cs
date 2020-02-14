using Domain.Entities;
using System;

namespace Domain.Services.Interfaces
{
    public interface ITransactionService
    {
        public TransactionResponse Cancel(Guid PaymentId);
        public TransactionResponse Get(Guid PaymentId, bool Callback);
    }
}
