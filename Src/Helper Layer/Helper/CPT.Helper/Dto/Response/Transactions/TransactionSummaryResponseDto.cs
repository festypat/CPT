using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CPT.Helper.Dto.Response.Transactions
{
    public class TransactionSummaryResponseDto
    {
        public long TotalTransaction { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class TransactionSummaryBaseResponseDto
    {
        public string ResponseCode { get; set; }
        public bool Success { get; set; }
        [JsonIgnore]
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public TransactionSummaryResponseDto data { get; set; }
    }
}
