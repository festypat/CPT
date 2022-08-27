using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPT.WebApp.Dto.Response
{
    public class Data
    {
        public string accesstoken { get; set; }
        public object tokenType { get; set; }
        public string message { get; set; }
        public string tokenExpire { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public object phoneNumber { get; set; }
        public int customerId { get; set; }
    }

    public class Result
    {
        public string responseCode { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
        public int customerId { get; set; }
        public Data data { get; set; }
    }

    public class UserInfoResponseDto
    {
        public bool success { get; set; }
        public Result result { get; set; }
    }
}
