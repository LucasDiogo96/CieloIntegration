using Newtonsoft.Json;

namespace Domain
{
    public class BankslipRequest
    {
        readonly Customer _customer;
        readonly BankslipPaymentRequest _payment;

        /// <summary>
        /// Transaction Information
        /// </summary>
        /// <param name="merchantOrderId">Order Identification</param>
        /// <param name="customer">Customer Information</param>
        /// <param name="payment">Payment Information</param>
        [JsonConstructor]
        public BankslipRequest(string merchantOrderId, Customer customer, BankslipPaymentRequest payment)
        {
            MerchantOrderId = merchantOrderId;
            _customer = customer;
            _payment = payment;
        }

        public string MerchantOrderId { get; set; }
        public Customer Customer
        {
            get { return _customer; }
        }
        public BankslipPaymentRequest Payment
        {
            get { return _payment; }
        }
    }

    public class BankslipPaymentRequest
    {
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
