using Domain.Entities;

namespace Domain.Services.Interfaces
{
    public interface ITokenizationService
    {
        TokenizationResponse Create(CreditCard creditCard);
    }
}
