using Newtonsoft.Json;

namespace Domain.Entities
{
    public class CreditCardRequest
    {
        readonly Customer _customer;
        readonly CreditCardPaymentRequest _payment;

        [JsonConstructor]
        public CreditCardRequest(string merchantOrderId, Customer customer, CreditCardPaymentRequest payment)
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
        public CreditCardPaymentRequest Payment
        {
            get { return _payment; }
        }
    }  
}
