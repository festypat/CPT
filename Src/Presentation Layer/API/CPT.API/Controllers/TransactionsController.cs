using CPT.ApplicationCore.Services.Transaction.Interface;
using CPT.Helper.Notification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPT.API.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionsController : BaseController
    {
        private readonly ITransactionService _transactionService;
        public TransactionsController(INotificationTask notification,
          ITransactionService transactionService) : base(notification)
        {
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
        }

        [HttpGet]
        [Route("summary")]
        public async Task<IActionResult> TransactionSummary([FromQuery] long customerId) => Response(await _transactionService.TransactionSummary(customerId).ConfigureAwait(false));

        [HttpGet]
        [Route("history")]
        public async Task<IActionResult> TransactionHistory([FromQuery] long customerId) => Response(await _transactionService.TransactionHistory(customerId).ConfigureAwait(false));
    }
}
