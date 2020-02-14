namespace Domain.Entities
{
    public class Callback
    {
        public string RecurrentPaymentId { get; set; }
        public string PaymentId { get; set; }
        public int ChangeType { get; set; }
    }
}

