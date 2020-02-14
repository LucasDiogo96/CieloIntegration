using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Cielo.Request.QrCode
{
    public class QrCodePaymentRequest
    {
        public string Type { get; set; }
        public int Amount { get; set; }
        public int Installments { get; set; }
        public bool Capture { get; set; }
    }
}
