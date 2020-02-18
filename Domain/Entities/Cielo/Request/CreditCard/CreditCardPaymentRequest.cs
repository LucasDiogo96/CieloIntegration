using Domain.Enums;
using Lib.Util;
using System;

namespace Domain.Entities
{
    public class CreditCardPaymentRequest
    {
        readonly CreditCard _creditCard;
        readonly DebitCard _debitcard;


        public CreditCardPaymentRequest(Transaction<DebitCard> transaction, Configuration _cieloConfiguration)
        {

            if (transaction.PaymentObject == null)
                throw new ArgumentNullException(paramName: nameof(transaction.PaymentObject), message: "O cartão de débito deve ser informado.");

            Amount = (int)(transaction.Amount * 100);
            Currency = Domain.Enums.Currency.BRL.ToString();
            Country = "BRA";
            Type = PaymentType.DebitCard.ToString();
            Authenticate = true;
            SoftDescriptor = _cieloConfiguration.SoftDescriptor;
            ReturnUrl = "http://www.cielo.com.br";
            DebitCard = (
              new DebitCard()
              {
                  CardNumber = transaction.PaymentObject.CardNumber,
                  Holder = transaction.PaymentObject.Holder,
                  SecurityCode = transaction.PaymentObject.SecurityCode,
                  Brand = CreditCardUtil.GetBrand(transaction.PaymentObject.CardNumber),
                  ExpirationDate = transaction.PaymentObject.ExpirationDate
              }
              );
        }

        public CreditCardPaymentRequest(Transaction<CreditCard> transaction, Configuration _cieloConfiguration)
        {

            if (transaction.PaymentObject == null)
                throw new ArgumentNullException(paramName: nameof(transaction.PaymentObject), message: "O cartão de crédito deve ser informado.");

            Amount = (int)(transaction.Amount * 100);
            Currency = Domain.Enums.Currency.BRL.ToString();
            Authenticate = false;
            Country = "BRA";
            Type = PaymentType.CreditCard.ToString();
            Installments = transaction.PaymentObject.Installments;
            SoftDescriptor = _cieloConfiguration.SoftDescriptor;
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
              );
        }

        public CreditCardPaymentRequest() { }

        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Country { get; set; }
        public string Provider { get; set; }
        public int ServiceTaxAmount { get; set; }
        public string SoftDescriptor { get; set; }
        public int Installments { get; set; }
        public bool Capture { get; set; }
        public bool Authenticate { get; set; }

        public string ReturnUrl { get; set; }
        public CreditCard CreditCard { get; set; }
        public DebitCard DebitCard { get; set; }
    }
}
