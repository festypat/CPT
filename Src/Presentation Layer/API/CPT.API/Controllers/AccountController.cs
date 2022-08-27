using CPT.ApplicationCore.Services.Account.Interface;
using CPT.Helper.Dto.Request.Account;
using CPT.Helper.Notification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPT.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
      
        public AccountController(INotificationTask notification,
            IAccountService accountService) : base(notification)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        [HttpPost]
        [Route("create-account")]
        public async Task<IActionResult> CreateAccount([FromBody] RegisterUserRequestDto request) => Response(await _accountService.RegisterUserAsync(request).ConfigureAwait(false));

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request) => Response(await _accountService.AuthenticateUser(request).ConfigureAwait(false));
    }
}
