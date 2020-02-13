namespace Domain
{
    public class Address
    {
        #region ctor

        /// <summary>
        /// Address Information
        /// </summary>
        /// <param name="street">Street</param>
        /// <param name="number">Number</param>
        /// <param name="complement">Complement</param>
        /// <param name="zipCode">Zip Code</param>
        /// <param name="city">City</param>
        /// <param name="state">State</param>
        /// <param name="country">Country</param>
        public Address(string street, string number, string complement, string zipCode, string city, string state, string country = "BRA")
        {
            Street = street;
            Number = number;
            Complement = complement;
            ZipCode = zipCode.ToNumbers();
            City = city;
            State = state;
            Country = country.ToString();
        }

        public Address() { }

        #endregion

        #region properties

        public string Street { get;  set; }
        public string Number { get;  set; }
        public string Complement { get;  set; }
        public string ZipCode { get;  set; }
        public string District { get; set; }
        public string City { get;  set; }
        public string State { get;  set; }
        public string Country { get;  set; }

        #endregion
    }

}
