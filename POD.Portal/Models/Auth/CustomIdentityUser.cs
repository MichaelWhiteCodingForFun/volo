using Microsoft.AspNet.Identity;
using POD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace POD.Portal.Models.Auth
{
    public class CustomIdentityUser : IUser
    {
        private User dbUser;

        //private string userName = string.Empty;
        //public string Email = string.Empty;
        //public int OrganizationID = 0;
        ////private string id = string.Empty;
        //public int RoleID = 0;

        public string Id { get { return this.dbUser.UserID.ToString(); } }
          

        public string UserName
        {
            get { return this.dbUser.UserName; }
            set
            {
                this.dbUser.UserName = value;
            }
        }

        public CustomIdentityUser(string userName, string fullName, string organizationCode)
        {
            this.dbUser = new User
            {
                //UserID = Guid.NewGuid(),
                UserName = userName,
                FullName = fullName,
                Email = userName,
                OrganizationCode = organizationCode
            };
        }

        public Task<ClaimsIdentity> GenerateUserIdentityAsync(CustomIdentityUserManager manager)
        {
            return Task.FromResult(GenerateUserIdentity(manager));
        }

        public ClaimsIdentity GenerateUserIdentity(CustomIdentityUserManager manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

    }
}