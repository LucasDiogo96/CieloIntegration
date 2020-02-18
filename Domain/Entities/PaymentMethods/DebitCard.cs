using Domain.Interfaces;

namespace Domain.Entities
{
    public class DebitCard : ICard
    {
        public string CardNumber { get; set; }
        public string Holder { get; set; }
        public string ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
        public string Brand { get; set; }
        public string AuthenticationUrl { get; set; }
    }
}
