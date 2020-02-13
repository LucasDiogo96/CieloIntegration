using Solar.Domain.Entities;
using Solar.Domain.Entities.Cielo;
using Solar.Domain.Entities.Transaction;

namespace Solar.Domain.Interfaces.Services
{
    public interface IPaymentService
    {
        TransactionResponse CreateTransactionCreditCard(Transaction<CreditCard> transaction);
        TransactionResponse CreateTransactionBankslip(Transaction<Bankslip> transaction);
    }
}
