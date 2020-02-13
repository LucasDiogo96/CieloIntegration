using System;

namespace Domain
{
    public class Customer
    {
        #region ctor

        /// <summary>
        /// Customer Information
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="identity">Identity Data</param>
        /// <param name="identityType">Identity Type</param>
        /// <param name="birthdate">Birthday</param>
        /// <param name="address">Address</param>
        /// <param name="deliveryAddress">Deliery Address</param>
        public Customer(string name, string identity = null, CustomerIdentityType identityType = CustomerIdentityType.Default, DateTime? birthdate = null, Address address = null, Address deliveryAddress = null)
        {
            Name = name;
            Identity = identity?.RegexReplace(@"[\/.-]*", String.Empty);
            IdentityType = identityType.ToDescription();
            Birthdate = birthdate.ToCieloShortFormatDate();
            Address = address;
        }
        public Customer() { }


        #endregion

        #region properties

        public string Name { get; set; }
        public string Identity { get; set; }
        public string Email { get; set; }
        public string IdentityType { get; set; }
        public string Birthdate { get; set; }
        public Address Address { get; set; }
        #endregion
    }
}
