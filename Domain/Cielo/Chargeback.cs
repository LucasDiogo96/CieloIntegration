namespace Domain
{
    public class Chargeback
    {
        public int Amount { get; set; }
        public string CaseNumber { get; set; }
        public string Date { get; set; }
        public string ReasonCode { get; set; }
        public string ReasonMessage { get; set; }
        public string Status { get; set; }
        public string RawData { get; set; }
    }
}
