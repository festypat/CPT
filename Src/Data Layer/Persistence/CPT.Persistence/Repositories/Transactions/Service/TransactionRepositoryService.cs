using CPT.Helper.Dto.Response;
using CPT.Helper.Dto.Response.Transactions;
using CPT.Persistence.Context;
using CPT.Persistence.Repositories.Transactions.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPT.Persistence.Repositories.Transactions.Service
{
    public class TransactionRepositoryService : ITransactionRepositoryService
    {
        private readonly CPTDbContext _context;
        public TransactionRepositoryService(CPTDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<TransactionSummaryBaseResponseDto> TransactionSummary(long customerId)
        {
            try
            {
                var query = await _context.Transactions.Where(t => t.CustomerId == customerId).ToListAsync();
                
                //var totalTransactions = await _context.Transactions.Where(t => t.CustomerId == customerId).SumAsync(x => x.Amount);
                var totalTransactions = await _context.Transactions.Where(t => t.CustomerId == customerId).SumAsync(x => x.Amount);

                return new TransactionSummaryBaseResponseDto
                {
                    StatusCode = HttpResponseCodes.Success,
                    Message = "Success",
                    Success = true,
                    ResponseCode = "00",
                    data = new TransactionSummaryResponseDto
                    {
                        TotalAmount = query.Sum(x=>x.TotalAmount),
                        TotalTransaction = query.Count()
                    }
                };
            }
            catch (Exception ex)
            {
                return new TransactionSummaryBaseResponseDto
                {
                    StatusCode = HttpResponseCodes.InternalError,
                    Message = "Internal error occured",
                    Success = true,
                    ResponseCode = "01"                    
                };
            }
        }

        public async Task<TransactionHistoryBaseResponseDto> TransactionHistory(long customerId)
        {
            try
            {
                var query = await (from t in _context.Transactions
                                   join p in _context.Products on t.ProductId equals p.ProductId
                                   join c in _context.Customers on t.CustomerId equals c.CustomerId

                                   where t.CustomerId == customerId
                                   select new TransactionHistoryResponseDto
                                   {
                                       Amount = t.Amount,
                                       Qty = t.Qty,
                                       TotalAmount = t.TotalAmount,
                                       CustomerId = customerId,
                                       ProductName = p.ProductName,
                                       ProductCode = p.ProductCode,
                                       FullName = $"{c.FirstName}{" "}{c.LastName}",
                                       DateOfBirth = c.DateOfBirth,
                                       Phone = c.PhoneNumber                                       
                                   }).ToListAsync();

                return new TransactionHistoryBaseResponseDto
                {
                    StatusCode = HttpResponseCodes.Success,
                    Message = "Success",
                    Success = true,
                    ResponseCode = "00",
                    data = query
                };

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
