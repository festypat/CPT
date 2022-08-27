using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CPT.Helper.Dto.Response.Transactions
{
    public class TransactionHistoryResponseDto
    {
        public int TransactionId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string FullName { get; set; }
        public string DateOfBirth { get; set; }
        public string Phone { get; set; }
        public long CustomerId { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; }
        public int Qty { get; set; }

    }

    public class TransactionHistoryBaseResponseDto
    {
        public string ResponseCode { get; set; }
        public bool Success { get; set; }
        [JsonIgnore]
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public IEnumerable<TransactionHistoryResponseDto> data { get; set; }
    }
}
