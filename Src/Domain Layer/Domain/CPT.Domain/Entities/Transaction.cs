using System;
using System.Collections.Generic;
using System.Text;

namespace CPT.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public long TransactionId { get; set; }
        public long ProductId { get; set; }
        public long CustomerId { get; set; }
        public int Qty { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime DateEntered { get; set; } = DateTime.Now;
        public Product Product { get; set; }
        public Customer Customer { get; set; }
    }
}
