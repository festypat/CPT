using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPT.WebApp.ViewModel
{
    public class UserDetailsViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string Role { get; set; }
        public long CustomerId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
