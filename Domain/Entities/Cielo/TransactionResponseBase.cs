using Domain.Entities;

namespace Domain
{
    public class CheckTransactionResponseBase
    {
        public string ReasonCode { get; set; }
        public string ReasonMessage { get; set; }
        public string MerchantOrderId { get; set; }
        public Customer Customer { get; set; }
    }
}
