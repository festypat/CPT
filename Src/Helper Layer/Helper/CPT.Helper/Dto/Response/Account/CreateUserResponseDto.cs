using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CPT.Helper.Dto.Response.Account
{
    public class CreateUserResponseDto
    {
        public string Username { get; set; }
        public string Role { get; set; }
    }

    public class CreateUserBaseResponseDto
    {
        public string ResponseCode { get; set; }
        public bool Success { get; set; }
        [JsonIgnore]
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public CreateUserResponseDto data { get; set; }
    }
}
