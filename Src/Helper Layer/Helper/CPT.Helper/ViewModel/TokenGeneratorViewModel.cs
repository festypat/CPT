using System;
using System.Collections.Generic;
using System.Text;

namespace CPT.Helper.ViewModel
{
    public class TokenGeneratorViewModel
    {
        public string ResponseCode { get; set; }
        public string Accesstoken { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string TokenType { get; set; }
        public string Message { get; set; }
        public string TokenExpire { get; set; }
        public string Role { get; set; }
        public long CustomerId { get; set; }
    }
}
