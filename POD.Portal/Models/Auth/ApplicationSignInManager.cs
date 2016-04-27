using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace POD.Portal.Models.Auth
{
    public class ApplicationSignInManager : SignInManager<CustomIdentityUser, string>
    {
        public ApplicationSignInManager(CustomIdentityUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(CustomIdentityUser user)
        {
            return user.GenerateUserIdentityAsync((CustomIdentityUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<CustomIdentityUserManager>(), context.Authentication);
        }
    }
}