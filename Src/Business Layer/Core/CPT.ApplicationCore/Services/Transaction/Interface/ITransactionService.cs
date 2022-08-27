using CPT.Helper.Dto.Response.Transactions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CPT.ApplicationCore.Services.Transaction.Interface
{
    public interface ITransactionService
    {
        Task<TransactionSummaryBaseResponseDto> TransactionSummary(long customerId);
        Task<TransactionHistoryBaseResponseDto> TransactionHistory(long customerId);
    }
}
