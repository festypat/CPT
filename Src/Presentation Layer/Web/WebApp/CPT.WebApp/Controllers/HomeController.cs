using CPT.WebApp.Extenstions;
using CPT.WebApp.Models;
using CPT.WebApp.Services.Transaction;
using CPT.WebApp.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CPT.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CustomerTransactions _customerTransactions;
        public HomeController(ILogger<HomeController> logger, CustomerTransactions customerTransactions)
        {
            _logger = logger;
            _customerTransactions = customerTransactions ?? throw new ArgumentNullException(nameof(customerTransactions));
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Dashboard()
        {
            var username = User.GetSessionDetails().Username;
            var customerId = User.GetSessionDetails().CustomerId;

            var totalTrans = await _customerTransactions.TransactionSummary(customerId);         

            return View(totalTrans);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
