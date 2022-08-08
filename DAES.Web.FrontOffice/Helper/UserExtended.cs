using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace DAES.Web.FrontOffice.Helper
{
    public static class UserExtended
    {
        public static string Email(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.Email);
            return claim == null ? null : claim.Value;
        }
    }
}
