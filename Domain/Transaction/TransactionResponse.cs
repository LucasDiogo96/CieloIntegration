using System;

namespace Domain
{
    public class TransactionResponse
    {
        public Object Detail { get; set; }
        public bool HasError { get; set; }
    }
}
