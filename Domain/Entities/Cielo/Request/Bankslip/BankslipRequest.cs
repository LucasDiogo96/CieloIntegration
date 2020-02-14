using Newtonsoft.Json;

namespace Domain.Entities
{
    public class BankslipRequest
    {
        readonly Customer _customer;
        readonly BankslipPaymentRequest _payment;


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

   
}
