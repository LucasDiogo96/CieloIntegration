namespace Domain.Entities
{
    public class PaymentMethodResponse
    {
        public string MerchantOrderId { get; set; }
        public Customer Customer { get; set; }
    }
}
