using Domain.Enums;
using Solar.Domain.Enums;
using System;

namespace Domain.Entities
{
    public class TransactionBase
    {
        public Guid IdTransaction { get; set; }
        public string OrderNumber { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public Customer Customer { get; set; }
        public PaymentType PaymentType { get; set; }
        public Status TransactionStatus { get; set; }
        public string CreatedDate { get; set; }
    }
}
