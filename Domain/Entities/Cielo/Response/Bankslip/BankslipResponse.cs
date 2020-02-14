namespace Domain.Entities
{
    public class BankslipResponse : PaymentMethodResponse
    {
        public BankslipPaymentResponse Payment { get; set; }
    }
}