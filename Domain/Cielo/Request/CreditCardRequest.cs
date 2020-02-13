using Solar.Domain.Enums;
using System;

namespace Domain
{
    public class CreditCardRequest
    {
        #region  vars

        readonly CreditCard _creditCard;
        readonly DebitCard _debitCard;

        #endregion

        #region ctor

        /// <summary>
        /// Payment Information
        /// </summary>
        /// <param name="type">Type. Eg: CredictCard, DebitCard, Eletronic Transfer, etc.</param>
        /// <param name="amount">Total purchase value</param>
        /// <param name="provider">Provider name</param>
        /// <param name="returnUrl">Url which user will be redirect after finish the payment process.</param>
        public CreditCardRequest(PaymentType type,
                       decimal amount,
                       EletronicTransferProvider provider,
                       string returnUrl)
        {
            Type = type.ToString();
            Amount = (int)(amount * 100);
            Provider = provider.ToString();
            ReturnUrl = returnUrl;
        }

        /// <summary>
        /// Payment Information
        /// </summary>
        /// <param name="type">Type. Eg: CredictCard, DebitCard, Eletronic Transfer, etc.</param>
        /// <param name="amount">Total purchase value</param>
        /// <param name="installments">Installments</param>
        /// <param name="softDescriptor">Text to be printed in the Bank Invoice</param>
        /// <param name="capture">Should capture payment</param>
        /// <param name="authenticate">Should redirect to the Bank to authenticate card.</param>
        /// <param name="returnUrl">Url which user will be redirect after finish the payment process.</param>
        /// <param name="creditCard">Credit Card Information</param>
        /// <param name="debitCard">Debit Card Information</param>
        public CreditCardRequest(PaymentType type,
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

        public CreditCardRequest() { }

        #endregion

        #region properties

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
        public DebitCard DebitCard
        {
            get { return _debitCard; }
        }

        #endregion
    }
}
