using Domain.Entities;

namespace Domain.Services.Interfaces
{
    public interface IPaymentService
    {
        TransactionResponseDetail CreateTransactionCreditCard(Transaction<CreditCard> transaction);
        TransactionResponseDetail CreateTransactionBankslip(Transaction<Bankslip> transaction);
        TransactionResponseDetail CreateTransactionQRCode(Transaction<QrCode> transaction);
    }
}
