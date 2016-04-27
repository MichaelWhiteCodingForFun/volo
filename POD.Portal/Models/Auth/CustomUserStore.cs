using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace POD.Portal.Models.Auth
{
    //public class CustomUserSore<T> : IUserStore<T> where T : CustomCustomIdentityUser
    //{

    //    System.Threading.Tasks.Task IUserStore<T>.CreateAsync(T user)
    //    {
    //        //Create /Register New User 
    //        throw new NotImplementedException();
    //    }

    //    System.Threading.Tasks.Task IUserStore<T>.DeleteAsync(T user)
    //    {
    //        //Delete User 
    //        throw new NotImplementedException();
    //    }

    //    System.Threading.Tasks.Task<T> IUserStore<T>.FindByIdAsync(string userId)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    System.Threading.Tasks.Task<T> IUserStore<T>.FindByNameAsync(string userName)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    System.Threading.Tasks.Task IUserStore<T>.UpdateAsync(T user)
    //    {
    //        //Update User Profile 
    //        throw new NotImplementedException();
    //    }

    //    void IDisposable.Dispose()
    //    {
    //        // throw new NotImplementedException(); 

    //    }
    //}

    public class CustomUserStore : IUserStore<CustomIdentityUser>
       , IUserLoginStore<CustomIdentityUser>
       , IUserPasswordStore<CustomIdentityUser>
       , IUserSecurityStampStore<CustomIdentityUser>
       , IUserClaimStore<CustomIdentityUser>
       , IUserEmailStore<CustomIdentityUser>
       , IUserLockoutStore<CustomIdentityUser, string>
       , IUserTwoFactorStore<CustomIdentityUser, string>
    {
        public CustomUserStore()
        {

        }

        public void Dispose()
        {

        }

        #region IUserStore
        public virtual Task CreateAsync(CustomIdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.Factory.StartNew(() =>
            {
                //using (var repository = MobizRepositoryFactory.GetForWrite())
                //{
                //    repository.Insert(user.MobizUser, user.UserName);
                //}
            });
        }

        public virtual Task DeleteAsync(CustomIdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.Factory.StartNew(() =>
            {
                //using (var repository = MobizRepositoryFactory.GetForWrite())
                //{
                //    repository.DeleteUser(user.MobizUser.Id);
                //}
            });
        }

        public virtual Task<CustomIdentityUser> FindByIdAsync(string userId)
        {
            return null;
            //if (string.IsNullOrWhiteSpace(userId))
            //    throw new ArgumentNullException("userId");

            //Guid parsedUserId;
            //if (!Guid.TryParse(userId, out parsedUserId))
            //    throw new ArgumentException(string.Format("'{0}' is not a valid ID.", new { userId }),
            //        DataErrorCodes.InvalidUserId.ToString());

            //return Task.Factory.StartNew(() =>
            //{
            //    using (var repository = MobizRepositoryFactory.GetForRead())
            //    {
            //        return new CustomIdentityUser((User)repository.GetUser(parsedUserId.ToString()));
            //    }
            //});
        }

        public virtual Task<CustomIdentityUser> FindByNameAsync(string userName)
        {
            return null;
            //if (string.IsNullOrWhiteSpace(userName))
            //    throw new ArgumentNullException("userName");

            //return Task.Factory.StartNew(() =>
            //{
            //    using (var repository = MobizRepositoryFactory.GetForRead())
            //    {
            //        var user = repository.GetUserByUsername(userName);
            //        return (user != null) ? new CustomIdentityUser(user) : null;
            //    }
            //});
        }

        public virtual Task UpdateAsync(CustomIdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.Factory.StartNew(() =>
            {
                //using (var repository = MobizRepositoryFactory.GetForWrite())
                //{
                //    repository.Update(user.MobizUser);
                //    // Update security information separately
                //    var security = repository as ISecurityRepository;
                //    security.UpdateUserSecurity(user.MobizUser);
                //}
            });
        }
        #endregion

        #region IUserLoginStore
        public virtual Task AddLoginAsync(CustomIdentityUser user, UserLoginInfo login)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (login == null)
                throw new ArgumentNullException("login");

            return Task.Factory.StartNew(() =>
            {
                //using (var repository = MobizRepositoryFactory.GetForWrite())
                //{
                //    UserExternalLogin uLogin = new UserExternalLogin
                //    {
                //        Id = Guid.NewGuid(),
                //        LoginProvider = login.LoginProvider,
                //        ProviderKey = login.ProviderKey,
                //        UserId = user.MobizUser.Id
                //    };
                //    repository.Insert(uLogin);
                //}
            });
        }

        public virtual Task<CustomIdentityUser> FindAsync(UserLoginInfo login)
        {
            if (login == null)
                throw new ArgumentNullException("login");

            return null;

            //return Task.Factory.StartNew(() =>
            //{
            //    using (var repository = MobizRepositoryFactory.GetForRead())
            //    {
            //        var userLogin = repository.GetUserLogin(login.LoginProvider, login.ProviderKey, true);
            //        if (userLogin == null)
            //            return null;

            //        return new CustomIdentityUser(userLogin.User);
            //    }
            //});
        }

        public virtual Task<IList<UserLoginInfo>> GetLoginsAsync(CustomIdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return null;

            //return Task.Factory.StartNew(() =>
            //{
            //    using (var repository = MobizRepositoryFactory.GetForRead())
            //    {
            //        IList<UserExternalLogin> dbLogins = repository.GetAllUserLogin(user.MobizUser.Id);
            //        IList<UserLoginInfo> idLogins = new List<UserLoginInfo>();
            //        foreach (var login in dbLogins)
            //        {
            //            idLogins.Add(
            //                new UserLoginInfo(login.LoginProvider, login.ProviderKey)
            //                );
            //        }
            //        return idLogins;
            //    }
            //});
        }

        public virtual Task RemoveLoginAsync(CustomIdentityUser user, UserLoginInfo login)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (login == null)
                throw new ArgumentNullException("login");

            return null;

            //return Task.Factory.StartNew(() =>
            //{
            //    using (var repository = MobizRepositoryFactory.GetForWrite())
            //    {
            //        //TODO:OPTIMIZE
            //        //TODO:VERIFY USER MATCHING
            //        var dbLogin = repository.GetUserLogin(login.LoginProvider, login.ProviderKey);
            //        repository.DeleteUserLogin(dbLogin.Id);
            //    }
            //});
        }
        #endregion

        #region IUserPasswordStore
        public virtual Task<string> GetPasswordHashAsync(CustomIdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            //return Task.FromResult(user.MobizUser.PswHash);
            return null;
        }

        public virtual Task<bool> HasPasswordAsync(CustomIdentityUser user)
        {
            //return Task.FromResult(!string.IsNullOrEmpty(user.MobizUser.PswHash));
            return null;
        }

        public virtual Task SetPasswordHashAsync(CustomIdentityUser user, string passwordHash)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            //user.MobizUser.PswHash = passwordHash;

            return Task.FromResult(0);
        }

        #endregion

        #region IUserSecurityStampStore
        public virtual Task<string> GetSecurityStampAsync(CustomIdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            //return Task.FromResult(user.MobizUser.SecurityStamp);
            return null;
        }

        public virtual Task SetSecurityStampAsync(CustomIdentityUser user, string stamp)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            //user.MobizUser.SecurityStamp = stamp;

            return Task.FromResult(0);
        }

        #endregion

        #region IUserClaimStore

        public Task AddClaimAsync(CustomIdentityUser user, System.Security.Claims.Claim claim)
        {
            //using (var repository = MobizRepositoryFactory.GetForWrite() as ISecurityRepository)
            //{
            //    IList<UserClaim> claims = repository.GetAllUserClaims(user.MobizUser.Id, claim.Type);
            //    if (!claims.Any(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value))
            //    {
            //        repository.Insert(new UserClaim
            //        {
            //            UserId = user.MobizUser.Id,
            //            ClaimType = claim.Type,
            //            ClaimValue = claim.Value
            //        });
            //    }

            //    return Task.FromResult(0);
            //}

            return null;
        }

        public Task<IList<System.Security.Claims.Claim>> GetClaimsAsync(CustomIdentityUser user)
        {
            //using (var repository = MobizRepositoryFactory.GetForRead() as ISecurityRepository)
            //{
            //    IList<UserClaim> claims = repository.GetAllUserClaims(user.MobizUser.Id);
            //    IList<System.Security.Claims.Claim> results = claims.Select(c => new
            //        System.Security.Claims.Claim(c.ClaimType, c.ClaimValue)).ToList();
            //    return Task.FromResult(results);

            //}

            return null;
        }

        public Task RemoveClaimAsync(CustomIdentityUser user, System.Security.Claims.Claim claim)
        {
            //using (var repository = MobizRepositoryFactory.GetForWrite() as ISecurityRepository)
            //{
            //    repository.DeleteUserClaim(user.MobizUser.Id, claim.Type, claim.Value);
            //    return Task.FromResult(0);
            //}

            return null;
        }

        #endregion

        #region IUserEmailStore
        public Task<CustomIdentityUser> FindByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("email");

            //return Task.Factory.StartNew(() =>
            //{
            //    using (var repository = MobizRepositoryFactory.GetForRead())
            //    {
            //        var user = repository.GetUserByEmail(email);
            //        return (user != null) ? new CustomIdentityUser(user) : null;
            //    }
            //});

            return null;
        }

        public Task<string> GetEmailAsync(CustomIdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            //return Task.Factory.StartNew(() =>
            //{
            //    using (var repository = MobizRepositoryFactory.GetForRead())
            //    {
            //        var emailList = repository.GetAllUserEmails(user.MobizUser.Id);
            //        var userEmail = emailList.FirstOrDefault();
            //        return (userEmail != null) ? userEmail.Email : null;
            //    }
            //});

            return null;
        }

        public Task<bool> GetEmailConfirmedAsync(CustomIdentityUser user)
        {
            //return Task.Factory.StartNew(() =>
            //{
            //    using (var repository = MobizRepositoryFactory.GetForRead())
            //    {
            //        var emailList = repository.GetAllUserEmails(user.MobizUser.Id);
            //        var userEmail = emailList.Single(e => e.Email == user.MobizUser.DefaultEmail);

            //        return userEmail.Verified;
            //    }
            //});
            return null;
        }

        public Task SetEmailAsync(CustomIdentityUser user, string email)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(CustomIdentityUser user, bool confirmed)
        {
            //return Task.Factory.StartNew(() =>
            //{
            //    using (var repository = MobizRepositoryFactory.GetForWrite())
            //    {
            //        repository.VerifyUserEmail(user.Guid, user.UserName);
            //    }
            //});

            return null;
        }

        #endregion

        #region IUserLockoutStore
        public Task<int> GetAccessFailedCountAsync(CustomIdentityUser user)
        {
            // return Task.FromResult(user.MobizUser.AccessFailedCount);
            return null;
        }

        public Task<bool> GetLockoutEnabledAsync(CustomIdentityUser user)
        {
            //return Task.FromResult(user.MobizUser.LockoutEnabled);
            return null;
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(CustomIdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            //return
            //    Task.FromResult(user.MobizUser.LockoutEndDateUtc.HasValue
            //        ? new DateTimeOffset(DateTime.SpecifyKind(user.MobizUser.LockoutEndDateUtc.Value, DateTimeKind.Utc))
            //        : new DateTimeOffset());

            return null;
        }

        public Task<int> IncrementAccessFailedCountAsync(CustomIdentityUser user)
        {
            //return Task.FromResult(++user.MobizUser.AccessFailedCount);
            return null;
        }

        public Task ResetAccessFailedCountAsync(CustomIdentityUser user)
        {
           // user.MobizUser.AccessFailedCount = 0;
            return Task.FromResult(0);
        }

        public Task SetLockoutEnabledAsync(CustomIdentityUser user, bool enabled)
        {
            //user.MobizUser.LockoutEnabled = enabled;
            return Task.FromResult<bool>(false);
        }

        public Task SetLockoutEndDateAsync(CustomIdentityUser user, DateTimeOffset lockoutEnd)
        {
            //user.MobizUser.LockoutEndDateUtc = lockoutEnd == DateTimeOffset.MinValue ? (DateTime?)null : lockoutEnd.UtcDateTime;
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(CustomIdentityUser user)
        {
            //return Task.FromResult(user.MobizUser.TwoFactorEnabled);
            return null;
        }

        public Task SetTwoFactorEnabledAsync(CustomIdentityUser user, bool enabled)
        {
            //user.MobizUser.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }
        #endregion
    }
}