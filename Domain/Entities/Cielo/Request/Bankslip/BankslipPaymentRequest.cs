using Domain.Enums;
using Lib.Extensions;
using System;

namespace Domain.Entities
{
    public class BankslipPaymentRequest
    {

        public BankslipPaymentRequest(ref Transaction<Bankslip> transaction, Configuration _cieloConfiguration)
        {
            if (transaction.PaymentObject == null)
                throw new ArgumentNullException(paramName: nameof(transaction.PaymentObject), message: "O boleto bancário deve ser informado.");

            Amount = (int)(transaction.Amount * 100);
            Identification = _cieloConfiguration.Identification;
            Assignor = _cieloConfiguration.Assignor;
            Provider = _cieloConfiguration.Provider;
            Address = _cieloConfiguration.Address;
            Amount = transaction.Amount;
            Type = PaymentType.Boleto.ToDescription();
            Instructions = transaction.PaymentObject.Instructions;
            BoletoNumber = transaction.PaymentObject.BoletoNumber;
            Demonstrative = transaction.PaymentObject.Demonstrative;
            ExpirationDate = transaction.PaymentObject.ExpirationDate;


            transaction.PaymentObject.Identification = Identification;
            transaction.PaymentObject.Assignor = Assignor;
            transaction.PaymentObject.Address = Address;
            transaction.PaymentObject.Provider = Provider;
        }



        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string Provider { get; set; }
        public string Address { get; set; }
        public string BoletoNumber { get; set; }
        public string Assignor { get; set; }
        public string Demonstrative { get; set; }
        public string ExpirationDate { get; set; }
        public string Identification { get; set; }
        public string Instructions { get; set; }
    }
}
