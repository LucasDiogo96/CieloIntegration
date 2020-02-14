using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Cielo.Response.QrCode
{
    public class QrCodePaymentResponse
    {
        public int ServiceTaxAmount { get; set; }
        public int Installments { get; set; }
        public int Interest { get; set; }
        public bool Capture { get; set; }
        public bool Authenticate { get; set; }
        public bool Recurrent { get; set; }
        public string Provider { get; set; }
        public int Amount { get; set; }
        public string ReceivedDate { get; set; }
        public int Status { get; set; }
        public bool IsSplitted { get; set; }
        public string QrCode { get; set; }
        public string ReturnMessage { get; set; }
        public string PaymentId { get; set; }
        public string Type { get; set; }
        public string Currency { get; set; }
        public string Country { get; set; }
        public List<LinkResponse> Links { get; set; }
    }
}
