using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Uppgift_ASP.NET_MVC.Data
{
    public class AppUserClaims : UserClaimsPrincipalFactory<AppUser, IdentityRole>
    {
        public AppUserClaims(
           UserManager<AppUser> userManager,
           RoleManager<IdentityRole> roleManager,
           IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
        {
            // sparar alla standardClaims som den genererar i identity
            var identity = await base.GenerateClaimsAsync(user);
            // lägger till skapar egna claims   kye value pair  en typ och dess värde
            identity.AddClaim(new Claim("DisplayName", user.DisplayName ?? ""));
            identity.AddClaim(new Claim("FirstName", user.FirstName));
            // finns som ClaimTypes
            identity.AddClaim(new Claim(ClaimTypes.Surname, user.LastName));
            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));


            return identity;
        }
    }
}
