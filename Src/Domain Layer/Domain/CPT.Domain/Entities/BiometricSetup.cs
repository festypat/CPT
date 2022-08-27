using System;
using System.Collections.Generic;
using System.Text;

namespace CPT.Domain.Entities
{
    public class BiometricSetup
    {
        public long BiometricSetupId { get; set; }
        public long CustomerId { get; set; }
        public string BiometricKey { get; set; }
        public string Device_UniqueID { get; set; }
        public string Device_Name { get; set; }
        public string Device_Model { get; set; }
        public string Platform_OS { get; set; }
        public bool IsRegisterOrUpdate { get; set; }
        public DateTime DateEntered { get; set; } = DateTime.Now;
        public DateTime LastDateModified { get; set; } 
        public Customer Customer { get; set; }
    }
}
