using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CPT.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Reference { get; set; }
        public string UserId { get; set; }
        public string UserCategory { get; set; }
        public string EmailAddress { get; set; }
        public string RegistrationStatus { get; set; }
        public bool IsLocked { get; set; }
        public bool AccountStatus { get; set; }
        public DateTime LastDateModified { get; set; }
        public DateTime DateRegistered { get; set; } = DateTime.Now;

    }
}
