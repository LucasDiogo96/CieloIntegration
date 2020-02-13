namespace Domain
{
    public class CheckTransaction<T>  : CheckTransactionResponseBase where T : new()
    {
        //public List<T> Payments { get; set; }
        public T Payment { get; set; }

    }
}
