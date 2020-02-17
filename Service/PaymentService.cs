using Domain.Entities;
using Domain.Enums;
using Domain.Services.Interfaces;
using Lib.Extensions;
using Lib.Util;
using log4net;
using Service;
using System;

namespace Services
{
    public class PaymentService : IPaymentService
    {
        protected internal Configuration _cieloConfiguration;
        protected internal ICieloService _cieloService;
        protected internal ITransactionService _transactionService;

        public PaymentService(Configuration cieloConfiguration)
        {
            _cieloConfiguration = cieloConfiguration;
            _cieloService = new CieloService(cieloConfiguration);
            _transactionService = new TransactionService(cieloConfiguration);
        }

        #region Payment Methods Transaction
        public TransactionResponseDetail CreateTransactionCreditCard(Transaction<CreditCard> transaction)
        {
            TransactionResponseDetail TResponse = new TransactionResponseDetail();

            var payment = LoadCredit(transaction);

            var request = new CreditCardRequest(transaction.OrderNumber, transaction.Customer, payment);

            //Captura e reserva o saldo do cartão de crédito
            var capture = _cieloService.CreateCreditTransaction(request);


            try
            {
                //set payment captured status
                transaction.TransactionStatus = (Status)capture.Payment.Status;

                switch (transaction.TransactionStatus)
                {
                    case Status.Authorized:

                        transaction.PaymentObject.CardNumber = capture.Payment.CreditCard.CardNumber;
                        transaction.PaymentObject.CardToken = capture.Payment.CreditCard.CardToken;
                        transaction.PaymentObject.Brand = payment.CreditCard.Brand;
                        transaction.PaymentObject.SecurityCode = null;
                        transaction.PaymentObject.ExpirationDate = null;

                        //Realiza a transação
                        CieloResponse response = _cieloService.CaptureTransaction(new Guid(capture.Payment.PaymentId));
                        transaction.TransactionStatus = response.Status;
                        transaction.IdTransaction = new Guid(capture.Payment.PaymentId);
                        transaction.CreatedDate = DateTime.Now.ToCieloShortFormatDate();

                        TResponse.Detail = transaction;
                        TResponse.HasError = false;

                        break;

                    case Status.NotFinished:
                    case Status.Refunded:
                    case Status.Voided:
                    case Status.Aborted:
                    case Status.Denied:
                        TResponse.Detail = new
                        {
                            Status = transaction.TransactionStatus,
                            Description = EnumExtensions.Description(transaction.TransactionStatus),
                            Error = capture.Payment.ReturnMessage
                        };
                        TResponse.HasError = true;

                        break;

                    default:
                        break;
                }

            }
            catch (Exception e)
            {

                if (capture != null && capture.Payment != null)
                {
                    //if has error try cancel
                    TransactionResponseDetail response = _transactionService.Cancel(new Guid(capture.Payment.PaymentId));

                    TResponse.Detail = new
                    {
                        Status = transaction.TransactionStatus,
                        Description = "Ocorreu um erro ao processar a transação.",
                        Error = e.Message
                    };
                    TResponse.HasError = true;

                }
            }

            return TResponse;
        }
        public TransactionResponseDetail CreateTransactionDebitCard(Transaction<DebitCard> transaction)
        {
            TransactionResponseDetail TResponse = new TransactionResponseDetail();

            var payment = LoadDebit(transaction);

            var request = new CreditCardRequest(transaction.OrderNumber, transaction.Customer, payment);

            //Captura e reserva o saldo do cartão de crédito
            var capture = _cieloService.CreateCreditTransaction(request);


            try
            {
                //set payment captured status
                transaction.TransactionStatus = (Status)capture.Payment.Status;

                switch (transaction.TransactionStatus)
                {
                    case Status.Authorized:

                        transaction.PaymentObject.CardNumber = capture.Payment.CreditCard.CardNumber;
                        transaction.PaymentObject.Brand = payment.CreditCard.Brand;
                        transaction.PaymentObject.SecurityCode = null;
                        transaction.PaymentObject.ExpirationDate = null;

                        //Realiza a transação
                        CieloResponse response = _cieloService.CaptureTransaction(new Guid(capture.Payment.PaymentId));
                        transaction.TransactionStatus = response.Status;
                        transaction.IdTransaction = new Guid(capture.Payment.PaymentId);
                        transaction.CreatedDate = DateTime.Now.ToCieloShortFormatDate();

                        TResponse.Detail = transaction;
                        TResponse.HasError = false;

                        break;
                    case Status.PaymentConfirmed:

                        transaction.PaymentObject.CardNumber = capture.Payment.DebitCard.CardNumber;
                        transaction.PaymentObject.Brand = payment.DebitCard.Brand;
                        transaction.PaymentObject.SecurityCode = null;
                        transaction.PaymentObject.ExpirationDate = null;
                        transaction.IdTransaction = new Guid(capture.Payment.PaymentId);
                        transaction.CreatedDate = DateTime.Now.ToCieloShortFormatDate();

                        TResponse.Detail = transaction;
                        TResponse.HasError = false;

                        break;

                    case Status.NotFinished:
                    case Status.Refunded:
                    case Status.Voided:
                    case Status.Aborted:
                    case Status.Denied:
                        TResponse.Detail = new
                        {
                            Status = transaction.TransactionStatus,
                            Description = EnumExtensions.Description(transaction.TransactionStatus),
                            Error = capture.Payment.ReturnMessage
                        };
                        TResponse.HasError = true;

                        break;

                    default:
                        break;
                }

            }
            catch (Exception e)
            {

                if (capture != null && capture.Payment != null)
                {
                    //if has error try cancel
                    TransactionResponseDetail response = _transactionService.Cancel(new Guid(capture.Payment.PaymentId));

                    TResponse.Detail = new
                    {
                        Status = transaction.TransactionStatus,
                        Description = "Ocorreu um erro ao processar a transação.",
                        Error = e.Message
                    };
                    TResponse.HasError = true;

                }
            }

            return TResponse;
        }
        public TransactionResponseDetail CreateTransactionBankslip(Transaction<Bankslip> transaction)
        {

            TransactionResponseDetail TResponse = new TransactionResponseDetail();

            try
            {
                Customer customer = transaction.Customer;

                var payment = LoadBankslip(transaction);


                var request = new BankslipRequest(transaction.OrderNumber, customer, payment);


                var response = _cieloService.CreateBankslipTransaction(request);

                //set payment captured status
                transaction.TransactionStatus = (Status)response.Payment.Status;

                switch (transaction.TransactionStatus)
                {
                    case Status.Authorized:

                        transaction.PaymentObject.BarCodeNumber = response.Payment.BarCodeNumber;
                        transaction.PaymentObject.BoletoNumber = response.Payment.DigitableLine;
                        transaction.PaymentObject.DigitableLine = response.Payment.BarCodeNumber;
                        transaction.PaymentObject.Url = response.Payment.Url;
                        transaction.CreatedDate = DateTime.Now.ToCieloShortFormatDate();
                        transaction.IdTransaction = new Guid(response.Payment.PaymentId);


                        TResponse.Detail = transaction;
                        TResponse.HasError = false;

                        break;
                    case Status.NotFinished:
                    case Status.Refunded:
                    case Status.Voided:
                    case Status.Aborted:
                    case Status.Denied:

                        TResponse.Detail = new
                        {
                            Status = transaction.TransactionStatus,
                            Description = EnumExtensions.Description(transaction.TransactionStatus),
                            Error = "Ocorreu um erro ao processar o boleto bancário."
                        };
                        TResponse.HasError = true;

                        break;
                }
            }
            catch (Exception e)
            {
                TResponse.Detail = new
                {
                    Status = transaction.TransactionStatus,
                    Description = "Ocorreu um erro ao processar a transação.",
                    Error = e.Message
                };
                TResponse.HasError = true;

            }

            return TResponse;
        }

        #endregion

        #region Create Payload
        private CreditCardPaymentRequest LoadCredit(Transaction<CreditCard> transaction)
        {
            CreditCardPaymentRequest payment = new CreditCardPaymentRequest()
            {
                Amount = transaction.Amount,
                Currency = Currency.BRL.ToString(),
                Country = "BRA",
                Type = PaymentType.CreditCard.ToString(),
                Installments = transaction.PaymentObject.Installments,
                SoftDescriptor = _cieloConfiguration.SoftDescriptor,
                CreditCard = (
                  !string.IsNullOrWhiteSpace(transaction.PaymentObject.CardToken) ?
                  new CreditCard
                  {
                      CardToken = transaction.PaymentObject.CardToken,
                      SecurityCode = transaction.PaymentObject.SecurityCode,
                      Installments = transaction.PaymentObject.Installments,
                  } :
                   new CreditCard
                   {
                       CardNumber = transaction.PaymentObject.CardNumber,
                       Holder = transaction.PaymentObject.Holder,
                       Name = transaction.PaymentObject.Holder,
                       SecurityCode = transaction.PaymentObject.SecurityCode,
                       Installments = transaction.PaymentObject.Installments,
                       Brand = CreditCardUtil.GetBrand(transaction.PaymentObject.CardNumber),
                       ExpirationDate = transaction.PaymentObject.ExpirationDate,
                       SaveCard = transaction.PaymentObject.SaveCard
                   }
                  )
            };
            return payment;
        }
        private CreditCardPaymentRequest LoadDebit(Transaction<DebitCard> transaction)
        {
            CreditCardPaymentRequest payment = new CreditCardPaymentRequest()
            {
                Amount = transaction.Amount,
                Currency = Currency.BRL.ToString(),
                Country = "BRA",
                Type = PaymentType.DebitCard.ToString(),
                SoftDescriptor = _cieloConfiguration.SoftDescriptor,
                DebitCard = (
                  new DebitCard()
                  {
                      CardNumber = transaction.PaymentObject.CardNumber,
                      Holder = transaction.PaymentObject.Holder,
                      SecurityCode = transaction.PaymentObject.SecurityCode,
                      Brand = CreditCardUtil.GetBrand(transaction.PaymentObject.CardNumber),
                      ExpirationDate = transaction.PaymentObject.ExpirationDate
                  }
                  )
            };
            return payment;
        }
        private BankslipPaymentRequest LoadBankslip(Transaction<Bankslip> transaction)
        {
            BankslipPaymentRequest payment = new BankslipPaymentRequest()
            {
                Identification = _cieloConfiguration.Identification,
                Assignor = _cieloConfiguration.Assignor,
                Provider = _cieloConfiguration.Provider,
                Address = _cieloConfiguration.Address,
                Amount = transaction.Amount,
                Type = PaymentType.Boleto.ToDescription(),
                Instructions = transaction.PaymentObject.Instructions,
                BoletoNumber = transaction.PaymentObject.BoletoNumber,
                Demonstrative = transaction.PaymentObject.Demonstrative,
                ExpirationDate = transaction.PaymentObject.ExpirationDate,
            };


            transaction.PaymentObject.Identification = payment.Identification;
            transaction.PaymentObject.Assignor = payment.Assignor;
            transaction.PaymentObject.Address = payment.Address;
            transaction.PaymentObject.Provider = payment.Provider;

            return payment;
        }
        #endregion

    }
}
