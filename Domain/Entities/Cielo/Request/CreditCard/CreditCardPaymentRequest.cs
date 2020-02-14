using Domain.Enums;
using System;

namespace Domain.Entities
{
    public class CreditCardPaymentRequest
    {
        readonly CreditCard _creditCard;

        public CreditCardPaymentRequest(PaymentType type,
                       decimal amount,
                       EletronicTransferProvider provider,
                       string returnUrl)
        {
            Type = type.ToString();
            Amount = (int)(amount * 100);
            Provider = provider.ToString();
            ReturnUrl = returnUrl;
        }

        public CreditCardPaymentRequest(PaymentType type,
                       decimal amount,
                       int installments,
                       string softDescriptor,
                       bool capture = false,
                       bool authenticate = false,
                       string returnUrl = null,
                       CreditCard creditCard = null,
                       DebitCard debitCard = null)
        {
            Type = type.ToString();
            Amount = (int)(amount * 100);
            Installments = installments;
            SoftDescriptor = softDescriptor;
            Capture = capture;
            Authenticate = Authenticate;
            ReturnUrl = returnUrl;

            if (type == PaymentType.CreditCard)
                if (creditCard == null)
                    throw new ArgumentNullException(paramName: nameof(creditCard), message: "O cartão de crédito deve ser informado.");
                else
                    _creditCard = creditCard;
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
    }
}
