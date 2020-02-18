using Lib.Util;
using System;

namespace Domain.Entities
{
    public class Customer
    {
        private string _identity;

        public string Name { get; set; }
        public string IdentityType { get; private set; }
        public string Email { get; set; }
        public string Identity
        {
            get { return _identity; }
            set
            {
                if (Util.IdentityIsValid(value))
                {
                    _identity = value;

                    string type = Util.GetIdentityType(_identity);

                    this.IdentityType = (!string.IsNullOrWhiteSpace(type) ? type : throw new InvalidOperationException("Não foi possível identificar o tipo do documento"));
                }
                else
                    throw new InvalidOperationException("O documento informato é inválido");


            }
        }
        public string Birthdate { get; set; }
        public Address Address { get; set; }
    }
}
