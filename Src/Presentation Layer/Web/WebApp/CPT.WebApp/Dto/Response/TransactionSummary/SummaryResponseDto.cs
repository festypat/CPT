using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPT.WebApp.Dto.Response.TransactionSummary
{

    public class Data
    {
        public int totalTransaction { get; set; }
        public decimal totalAmount { get; set; }
    }

    public class Result
    {
        public string responseCode { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }

    public class SummaryResponseDto
    {
        public bool success { get; set; }
        public Result result { get; set; }
    }


}
