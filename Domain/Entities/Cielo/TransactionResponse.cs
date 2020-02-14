using System.Collections.Generic;

namespace Domain
{
    public class TransactionResponse<T>  : CheckTransactionResponseBase where T : new()
    {
        public List<T> Payments { get; set; }
        public T Payment { get; set; }
    }
}
