using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class QrCodeResponse : PaymentMethodResponse
    {
        public QrCodePaymentResponse Payment { get; set; }
    }
}
