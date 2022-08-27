using System;
using System.Collections.Generic;
using System.Text;

namespace CPT.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Product()
        {
            Transactions = new HashSet<Transaction>();
        }
        public long ProductId { get; set; }
        public Guid CustomerId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateEntered { get; set; } = DateTime.Now;
        public ICollection<Transaction> Transactions { get; set; }
    }
}
