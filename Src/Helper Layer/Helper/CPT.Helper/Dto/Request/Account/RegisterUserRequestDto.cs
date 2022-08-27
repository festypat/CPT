using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CPT.Helper.Dto.Request.Account
{
    public class RegisterUserRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public DateTime LastDateModified { get; set; }
    }
}
