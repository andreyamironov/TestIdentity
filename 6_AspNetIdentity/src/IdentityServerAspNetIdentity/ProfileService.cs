using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServerAspNetIdentity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity
{
    public class ProfileService : IProfileService
    {
        protected UserManager<ApplicationUser> _userManager;
        public const string PolicyClaimTypeName = "policy";
        public ProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            var getClaims = await _userManager.GetClaimsAsync(user);
        //    var claims = new List<Claim>
        //{
        //    new Claim(JwtClaimTypes.Profile, getClaims.Any() ? getClaims.First()?.Value : "empty")
        //};

            context.IssuedClaims.AddRange(getClaims.Where(c=>c.Type == PolicyClaimTypeName));
        }

        //public async Task IsActiveAsync(IsActiveContext context)
        //{
        //    var user = await _userManager.GetUserAsync(context.Subject);
        //    context.IsActive = (user != null) && user.LockoutEnabled;
        //}



        //public Task GetProfileDataAsync(ProfileDataRequestContext context)
        //{
        //    context.IssuedClaims.AddRange(context.Subject.Claims);

        //    return Task.FromResult(0);
        //}

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.FromResult(0);
        }
    }
}
