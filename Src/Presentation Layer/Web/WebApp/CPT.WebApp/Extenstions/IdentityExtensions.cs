using CPT.WebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace CPT.WebApp.Extenstions
{
    public static class IdentityExtensions
    {
        public static UserDetailsViewModel GetSessionDetails(this IPrincipal principal)
        {
            try
            {
                //var customerId = identity.Claims.FirstOrDefault(c => c.Type == nameof(Claimswrapper.CustomerId))?.Value.Decrypt(Settings.Instance.Utilities.ClaimsSecurityKey);

                var identity = (ClaimsIdentity)principal.Identity;

                var getData = new UserDetailsViewModel
                {
                    Email = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                    Username = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value,
                    Role = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value,
                    CustomerId = Convert.ToInt32(identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value),
                    // CustomerId = identity.Claims.FirstOrDefault(c => c.Type == (long)Convert.ToInt64(ClaimTypes.NameIdentifier))?.Value,
                    ////Fullname = identity.Claims.FirstOrDefault(c => c.Type == nameof(ClaimswrapperViewModel.Fullname))?.Value,
                    ////PhoneNumber = identity.Claims.FirstOrDefault(c => c.Type == nameof(ClaimswrapperViewModel.PhoneNumber))?.Value,
                };

                return getData;
            }
            catch (Exception)
            {

                return null;

            }

        }

        public static void ClearSessionDetails(this IPrincipal principal)
        {
            var identity = (ClaimsIdentity)principal.Identity;
            foreach (var claim in identity.Claims)
            {
                identity.TryRemoveClaim(claim);
            }
        }
    }

}
