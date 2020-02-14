using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Cielo.Response.QrCode
{
    public class QrCodeResponse : PaymentMethodResponse
    {
        public QrCodePaymentResponse Payment { get; set; }
    }
}
