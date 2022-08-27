using CPT.Helper.Dto.Response.Transactions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CPT.Persistence.Repositories.Transactions.Interface
{
    public interface ITransactionRepositoryService
    {
        Task<TransactionSummaryBaseResponseDto> TransactionSummary(long customerId);
        Task <TransactionHistoryBaseResponseDto> TransactionHistory(long customerId);
    }
}
