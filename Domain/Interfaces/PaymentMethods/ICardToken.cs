namespace Domain.Interfaces
{
    public interface ICardToken
    {
        string CardToken { get; }
        bool SaveCard { get; }
    }
}
