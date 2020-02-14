using System;

namespace Domain.Entities
{
    public class TransactionResponse
    {
        public Object Detail { get; set; }
        public bool HasError { get; set; }
    }
}
