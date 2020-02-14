using Domain.Interfaces;

namespace Domain.Entities
{
    public class CreditCard : ICard, ICardToken
    {
        public string Name { get; set; }
        public string CustomerName { get; set; }
        public string CardToken { get; set; }
        public int Installments { get; set; }
        public string CardNumber { get; set; }
        public string Holder { get; set; }
        public string ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
        public string Brand { get; set; }
        public bool SaveCard { get; set; }
    }
}
