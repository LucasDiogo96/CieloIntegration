using Microsoft.Extensions.Configuration;

namespace Domain.Entities
{
    public class Configuration
    {
        public string ApiVersion { get; set; }
        public string DefaultEndpoint { get; set; }
        public string QueryEndpoint { get; set; }
        public string MerchantId { get; set; }
        public string MerchantKey { get; set; }
        public string ReturnUrl { get; set; }
        public string SoftDescriptor { get; set; }
        public string Assignor { get; set; }
        public string Identification { get; set; }
        public string Address { get; set; }
        public string Provider { get; set; }
    }
}