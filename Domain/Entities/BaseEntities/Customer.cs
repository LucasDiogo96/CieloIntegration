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
        public string Identity { get; set; }
        public string Birthdate { get; set; }
        public Address Address { get; set; }
    }
}
