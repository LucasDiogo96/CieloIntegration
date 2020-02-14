using System.Collections.Generic;

namespace Domain.Entities
{
    public class CreditCardPaymentResponse
    {
        public int ServiceTaxAmount { get; set; }
        public int Installments { get; set; }
        public string Interest { get; set; }
        public bool Capture { get; set; }
        public bool Authenticate { get; set; }
        public CreditCard CreditCard { get; set; }
        public bool IsCryptoCurrencyNegotiation { get; set; }
        public bool tryautomaticcancellation { get; set; }
        public string ProofOfSale { get; set; }
        public string Tid { get; set; }
        public string AuthorizationCode { get; set; }
        public string SoftDescriptor { get; set; }
        public string PaymentId { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public int CapturedAmount { get; set; }
        public string Country { get; set; }
        public List<Chargeback> Chargebacks { get; set; }
        public List<object> ExtraDataCollection { get; set; }
        public int Status { get; set; }
        public string ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
        public List<LinkResponse> Links { get; set; }
    }
}
