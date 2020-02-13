using Solar.Domain.Entities.Cielo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solar.Domain.Interfaces.Services
{
   public interface ITokenizationService
    {
        Tokenization Create(CreditCard creditCard);
    }
}
