using System;
using System.Collections.Generic;
using System.Text;

namespace CPT.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public Customer()
        {
            Transactions = new HashSet<Transaction>();
            BiometricSetup = new HashSet<BiometricSetup>();
        }
        public long CustomerId { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string DateEntered { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<BiometricSetup> BiometricSetup { get; set; }
    }

}
