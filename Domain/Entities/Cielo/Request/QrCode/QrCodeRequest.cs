using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Cielo.Request.QrCode
{
    using Newtonsoft.Json;

    namespace Domain.Entities
    {
        public class QrCodeRequest
        {
            readonly Customer _customer;
            readonly QrCodePaymentRequest _payment;

            [JsonConstructor]
            public QrCodeRequest(string merchantOrderId, Customer customer, QrCodePaymentRequest payment)
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
            public QrCodePaymentRequest Payment
            {
                get { return _payment; }
            }
        }
    }

}
