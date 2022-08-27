using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CPT.Helper.Dto.Response.Account
{
    public class LoginResponseDto
    {
        public string Accesstoken { get; set; }
        public string TokenType { get; set; }
        public string Message { get; set; }
        public string TokenExpire { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
        public long CustomerId { get; set; }
    }

    public class LoginBaseResponseDto
    {
        public string ResponseCode { get; set; }
        public bool Success { get; set; }
        [JsonIgnore]
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public long CustomerId { get; set; }
        public LoginResponseDto data { get; set; }
    }
}
