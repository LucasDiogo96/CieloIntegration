using System.Collections.Generic;

namespace Domain.Entities
{
    public class BankslipPaymentResponse
    {
        public string Instructions { get; set; }
        public string ExpirationDate { get; set; }
        public string Url { get; set; }
        public string Number { get; set; }
        public string BarCodeNumber { get; set; }
        public string DigitableLine { get; set; }
        public string Assignor { get; set; }
        public string Address { get; set; }
        public string Identification { get; set; }
        public string PaymentId { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string Country { get; set; }
        public string Provider { get; set; }
        public List<object> ExtraDataCollection { get; set; }
        public int Status { get; set; }
        public List<LinkResponse> Links { get; set; }
    }
}
