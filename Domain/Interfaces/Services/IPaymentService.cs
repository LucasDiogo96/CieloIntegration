using Domain.Entities;

namespace Domain.Services.Interfaces
{
    public interface IPaymentService
    {
        TransactionResponse CreateTransactionCreditCard(Transaction<CreditCard> transaction);
        TransactionResponse CreateTransactionBankslip(Transaction<Bankslip> transaction);
    }
}
