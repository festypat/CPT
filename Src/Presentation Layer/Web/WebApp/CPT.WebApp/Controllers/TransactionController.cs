using CPT.WebApp.Extenstions;
using CPT.WebApp.Services.Transaction;
using CPT.WebApp.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPT.WebApp.Controllers
{
    public class TransactionController : Controller
    {
        private readonly CustomerTransactions _customerTransactions;

        public TransactionController(CustomerTransactions customerTransactions)
        {
            _customerTransactions = customerTransactions ?? throw new ArgumentNullException(nameof(customerTransactions));
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Transactions()
        {
           // User.GetSessionDetails();

            var username = User.GetSessionDetails().Username;
            var customerId = User.GetSessionDetails().CustomerId;

            var totalTrans = await _customerTransactions.TransactionHistory(customerId);         

            return View(totalTrans);
        }
    }
}
