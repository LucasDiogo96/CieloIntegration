using Domain.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Domain
{
    public class TransactionCheckResponse
    {
        [JsonExtensionData]
        private IDictionary<string, object> _response;

        public TransactionCheckResponse()
        {
            _response = new Dictionary<string, object>();
        }

        public string MerchantOrderId { get; private set; }
        public string ProofOfSale { get; private set; }
        public string Tid { get; private set; }
        public string AuthorizationCode { get; private set; }
        public string AuthenticationUrl { get; private set; }
        public Guid PaymentId { get; private set; }
        public Status Status { get; private set; }
        public ReturnCode ReturnCode { get; private set; }
        public string ReturnMessage { get; private set; }
    }
}
