using CPT.ApplicationCore.Services.Account.Interface;
using CPT.Helper.Dto.Request.Account;
using CPT.Helper.Dto.Response;
using CPT.Helper.Dto.Response.Account;
using CPT.WebApp.Services.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CPT.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly LoginService _loginService;
        public AccountController(LoginService loginService)
        {
            _loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _loginService.AuthenticateUser(model);

                    if (user.Response == "00")
                    {
                        var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.CustomerId)));
                        identity.AddClaim(new Claim(ClaimTypes.Name, model.Username));
                        await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme,
                            new ClaimsPrincipal(identity));

                        return RedirectToAction("Dashboard", "Home");
                    }

                    else
                    {
                        ViewBag.Message = "Invalid login details";
                        return View();
                    }                    

                }

                return RedirectToAction("Fail");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Fail");
                //throw;
            }
        }

    }
}
