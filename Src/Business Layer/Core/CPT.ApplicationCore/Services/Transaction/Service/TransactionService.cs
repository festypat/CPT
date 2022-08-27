using CPT.ApplicationCore.Services.Transaction.Interface;
using CPT.Helper.Dto.Response.Transactions;
using CPT.Persistence.Repositories.Transactions.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CPT.ApplicationCore.Services.Transaction.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepositoryService _repositoryService;
        public TransactionService(ITransactionRepositoryService repositoryService)
        {
            _repositoryService = repositoryService ?? throw new ArgumentNullException(nameof(repositoryService));
        }

        public async Task<TransactionSummaryBaseResponseDto> TransactionSummary(long customerId)
        {
            try
            {
                return await _repositoryService.TransactionSummary(customerId);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<TransactionHistoryBaseResponseDto> TransactionHistory(long customerId)
        {
            try
            {
                var transactions = await _repositoryService.TransactionHistory(customerId);

                return transactions;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
