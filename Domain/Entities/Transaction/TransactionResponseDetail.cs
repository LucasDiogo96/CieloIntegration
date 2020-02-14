using System;

namespace Domain.Entities
{
    public class TransactionResponseDetail
    {
        public Object Detail { get; set; }
        public bool HasError { get; set; }
    }
}
