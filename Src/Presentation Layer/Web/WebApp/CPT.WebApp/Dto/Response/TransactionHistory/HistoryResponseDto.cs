using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPT.WebApp.Dto.Response.TransactionHistory
{

    public class Datum
    {
        public int transactionId { get; set; }
        public string productName { get; set; }
        public string productCode { get; set; }
        public string fullName { get; set; }
        public string dateOfBirth { get; set; }
        public string phone { get; set; }
        public int customerId { get; set; }
        public decimal amount { get; set; }
        public decimal totalAmount { get; set; }
        public int qty { get; set; }
    }

    public class Result
    {
        public string responseCode { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
        public List<Datum> data { get; set; }
    }

    public class HistoryResponseDto
    {
        public bool success { get; set; }
        public Result result { get; set; }
    }


}
