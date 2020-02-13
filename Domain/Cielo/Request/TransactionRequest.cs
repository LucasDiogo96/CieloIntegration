namespace Domain
{
    public class TransactionRequest
    {
        #region  vars

         readonly Customer _customer;
         readonly CreditCardRequest _payment;

        #endregion

        #region ctor

        /// <summary>
        /// Transaction Information
        /// </summary>
        /// <param name="merchantOrderId">Order Identification</param>
        /// <param name="customer">Customer Information</param>
        /// <param name="payment">Payment Information</param>
        [JsonConstructor]
        public TransactionRequest(string merchantOrderId, Customer customer, CreditCardRequest payment)
        {
            MerchantOrderId = merchantOrderId;
            _customer = customer;
            _payment = payment;
        }

        #endregion

        #region properties

        public string MerchantOrderId { get;  set; }
        public Customer Customer
        {
            get { return _customer; }
        }
        public CreditCardRequest Payment
        {
            get { return _payment; }
        }

        #endregion
    }
}
