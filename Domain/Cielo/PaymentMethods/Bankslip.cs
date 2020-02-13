using Domain.Interfaces;

namespace Domain
{
    public class Bankslip : IBankslip
    {
        public string Url { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        public string Provider { get; set; }
        public string Address { get; set; }
        public string BoletoNumber { get; set; }
        public string BarCodeNumber { get; set; }
        public string DigitableLine { get; set; }
        public string Assignor { get; set; }
        public string Demonstrative { get; set; }
        public string ExpirationDate { get; set; }
        public string Identification { get; set; }
        public string Instructions { get; set; }
    }
}
