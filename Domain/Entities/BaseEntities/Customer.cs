using System;

namespace Domain.Entities
{
    public class Customer
    {
        public string Name { get; set; }
        public string Identity { get; set; }
        public string Email { get; set; }
        public string IdentityType { get; set; }
        public string Birthdate { get; set; }
        public Address Address { get; set; }
    }
}
