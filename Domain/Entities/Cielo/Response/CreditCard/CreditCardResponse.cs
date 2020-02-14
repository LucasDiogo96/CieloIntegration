using System.Collections.Generic;

namespace Domain.Entities
{
    public class CreditCardResponse : PaymentMethodResponse
    {
        public CreditCardPaymentResponse Payment { get; set; }
    }
}