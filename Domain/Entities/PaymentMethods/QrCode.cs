namespace Domain.Entities
{
    public class QrCode
    {
        public string Type { get; set; }
        public int Amount { get; set; }
        public int Installments { get; set; }
        public bool Capture { get; set; }
    }
}
