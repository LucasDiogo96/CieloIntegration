using Solar.Domain.Entities.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solar.Domain.Interfaces.Services
{
    public interface ITransactionService
    {
        public TransactionResponse Cancel(Guid PaymentId);
        public TransactionResponse Get(Guid PaymentId, bool Callback);
    }
}
