using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Ninject;
using POD.Entities;
using POD.Interfaces;
using POD.Portal;
using POD.Portal.Models;
using POD.Portal.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Security;

//using AngularJSWebApplication.Filters;
//using AngularJSWebApplication.WebApiModels;
//using AngularJSWebApplication.Helpers;
//using AngularJSDataModels;
//using AngularJSApplicationService;
//using AngularJSDataServiceInterface;
//using AngularJSDataService;

namespace POD.Portal.Controllers.WebApi
{

    [RoutePrefix("api/accounts")]
    public class AccountController : ApiController
    {

        [Inject]
        public IAccountDataService _accountDataService { get; set; }

        public AccountController(IAccountDataService accountDataService)
        {
            _accountDataService = accountDataService;
        }

        ///// <summary>
        ///// Register User
        ///// </summary>
        ///// <param name="request"></param>
        ///// <param name="registerUserDTO"></param>
        ///// <returns></returns>
        //[Route("RegisterUser")]
        //[ValidateModelState]
        //[HttpPost]
        //public HttpResponseMessage RegisterUser(HttpRequestMessage request, [FromBody] UserDTO registerUserDTO)
        //{

        //    AccountsApiModel accountsWebApiModel = new AccountsApiModel();
        //    TransactionalInformation transaction = new TransactionalInformation();
        //    AccountsBusinessService accountsBusinessService;

        //    if (registerUserDTO.FirstName == null) registerUserDTO.FirstName = "";
        //    if (registerUserDTO.LastName == null) registerUserDTO.LastName = "";
        //    if (registerUserDTO.EmailAddress == null) registerUserDTO.EmailAddress = "";
        //    if (registerUserDTO.UserName == null) registerUserDTO.UserName = "";
        //    if (registerUserDTO.Password == null) registerUserDTO.Password = "";
        //    if (registerUserDTO.PasswordConfirmation == null) registerUserDTO.PasswordConfirmation = "";

        //    accountsBusinessService = new AccountsBusinessService(accountsDataService);
        //    User user = accountsBusinessService.RegisterUser(
        //        registerUserDTO.FirstName,
        //        registerUserDTO.LastName,
        //        registerUserDTO.UserName,
        //        registerUserDTO.EmailAddress,
        //        registerUserDTO.Password,
        //        registerUserDTO.PasswordConfirmation,
        //        out transaction);

        //    if (transaction.ReturnStatus == false)
        //    {
        //        accountsWebApiModel.ReturnMessage = transaction.ReturnMessage;
        //        accountsWebApiModel.ReturnStatus = transaction.ReturnStatus;
        //        accountsWebApiModel.ValidationErrors = transaction.ValidationErrors;
        //        var badResponse = Request.CreateResponse<AccountsApiModel>(HttpStatusCode.BadRequest, accountsWebApiModel);
        //        return badResponse;
        //    }

        //    ApplicationInitializationBusinessService initializationBusinessService;
        //    initializationBusinessService = new ApplicationInitializationBusinessService(applicationDataService);
        //    List<ApplicationMenu> menuItems = initializationBusinessService.GetMenuItems(true, out transaction);

        //    if (transaction.ReturnStatus == false)
        //    {
        //        accountsWebApiModel.ReturnMessage = transaction.ReturnMessage;
        //        accountsWebApiModel.ReturnStatus = transaction.ReturnStatus;
        //        var badResponse = Request.CreateResponse<AccountsApiModel>(HttpStatusCode.BadRequest, accountsWebApiModel);
        //        return badResponse;
        //    }

        //    accountsWebApiModel.IsAuthenicated = true;
        //    accountsWebApiModel.ReturnStatus = transaction.ReturnStatus;
        //    accountsWebApiModel.ReturnMessage.Add("Register User successful.");
        //    accountsWebApiModel.MenuItems = menuItems;
        //    accountsWebApiModel.User = user;

        //    FormsAuthentication.SetAuthCookie(user.UserId.ToString(), createPersistentCookie: false);

        //    var response = Request.CreateResponse<AccountsApiModel>(HttpStatusCode.OK, accountsWebApiModel);
        //    return response;


        //}


        //[Route("UpdateUser")]
        //[WebApiAuthenication]
        //[ValidateModelState]
        //[HttpPost]
        //public HttpResponseMessage UpdateUser(HttpRequestMessage request, [FromBody] UserDTO updateUserDTO)
        //{

        //    Guid userID = new Guid(User.Identity.Name);

        //    AccountsApiModel accountsWebApiModel = new AccountsApiModel();
        //    TransactionalInformation transaction = new TransactionalInformation();
        //    AccountsBusinessService accountsBusinessService;
        //    accountsWebApiModel.IsAuthenicated = true;

        //    if (updateUserDTO.FirstName == null) updateUserDTO.FirstName = "";
        //    if (updateUserDTO.LastName == null) updateUserDTO.LastName = "";
        //    if (updateUserDTO.EmailAddress == null) updateUserDTO.EmailAddress = "";
        //    if (updateUserDTO.UserName == null) updateUserDTO.UserName = "";
        //    if (updateUserDTO.Password == null) updateUserDTO.Password = "";
        //    if (updateUserDTO.PasswordConfirmation == null) updateUserDTO.PasswordConfirmation = "";

        //    accountsBusinessService = new AccountsBusinessService(accountsDataService);
        //    User user = accountsBusinessService.UpdateUser(
        //        userID,
        //        updateUserDTO.FirstName,
        //        updateUserDTO.LastName,
        //        updateUserDTO.UserName,
        //        updateUserDTO.EmailAddress,
        //        updateUserDTO.Password,
        //        updateUserDTO.PasswordConfirmation,
        //        out transaction);

        //    if (transaction.ReturnStatus == false)
        //    {
        //        accountsWebApiModel.ReturnMessage = transaction.ReturnMessage;
        //        accountsWebApiModel.ReturnStatus = transaction.ReturnStatus;
        //        accountsWebApiModel.ValidationErrors = transaction.ValidationErrors;
        //        var badResponse = Request.CreateResponse<AccountsApiModel>(HttpStatusCode.BadRequest, accountsWebApiModel);
        //        return badResponse;
        //    }

        //    accountsWebApiModel.ReturnStatus = transaction.ReturnStatus;
        //    accountsWebApiModel.ReturnMessage.Add("User successful updated.");

        //    var response = Request.CreateResponse<AccountsApiModel>(HttpStatusCode.OK, accountsWebApiModel);
        //    return response;

        //}

        private CustomIdentityUserManager _userManager;
        public CustomIdentityUserManager UserManager
        {
            get
            {
                //_userManager = new UserManager<IdentityUser>(new MobizUserStore<IdentityUser>(_ctx));
                return _userManager ?? Request.GetOwinContext().GetUserManager<CustomIdentityUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return Request.GetOwinContext().Authentication;
            }
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        [HttpPost]
        public async Task<IHttpActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //if (!model.IsAgree)
            //{
            //    ModelState.AddModelError("", Resources.acceptNeeded);
            //    return BadRequest(ModelState);
            //}
           // bool invitedUser = model.InviteId != null;
            var user = new CustomIdentityUser(model.UserName, model.FullName, model.OrganizationCode);
            var result = await UserManager.CreateAsync(user/*, model.Password, invitedUser*/);
            if (result.Succeeded)
            {
                //if (invitedUser)
                //{
                //    AddMembership(model, user.Id);
                //    return Ok("account/login");
                //}
                //else
                //{
                    await SendConfirmationEmail(user);
                    return Ok("account/displayEmail");
                //}
            }

            //IHttpActionResult errorResult = GetErrorResult(result);

            //if (errorResult != null)
            //{
            //    return errorResult;
            //}

            return Ok();
        }

        public async Task SendConfirmationEmail(CustomIdentityUser user)
        {
            if (user == null)
                return;
            var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            UriBuilder callbackUrl = new UriBuilder();
            callbackUrl.Scheme = this.Url.Request.RequestUri.Scheme;
            callbackUrl.Host = this.Url.Request.RequestUri.Host;
            callbackUrl.Path = "Account/ConfirmEmail";
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["userId"] = user.Id;
            query["code"] = code;
            callbackUrl.Query = query.ToString();
            await UserManager.SendEmailAsync(user.Id,
               Resource.EmailTempConfirmEmailSubject,
               string.Format(Resource.EmailTempConfirmEmailBody, callbackUrl));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ConfirmEmail")]
        public async Task<IHttpActionResult> ConfirmEmail(ConfirmEmailByCodeViewModel model)
        {
            var result = await UserManager.ConfirmEmailAsync(model.UserId, model.Code);
            if (!result.Succeeded)
                BadRequest();
            return Ok();
        }

        //[Route("Login")]
        ////[ValidateModelState]
        //[HttpPost]
        //public HttpResponseMessage Login(HttpRequestMessage request, [FromBody] LoginViewModel loginUser)
        //{

        //    //AccountsApiModel accountsWebApiModel = new AccountsApiModel();
        //    //TransactionalInformation transaction = new TransactionalInformation();
        //    //AccountsBusinessService accountsBusinessService;



        //    User user = _accountDataService.Login(
        //        loginUser.UserName,
        //        loginUser.Password);

        //    //if (transaction.ReturnStatus == false)
        //    //{
        //    //    accountsWebApiModel.ReturnMessage = transaction.ReturnMessage;
        //    //    accountsWebApiModel.ReturnStatus = transaction.ReturnStatus;
        //    //    accountsWebApiModel.ValidationErrors = transaction.ValidationErrors;
        //    //    var badResponse = Request.CreateResponse<AccountsApiModel>(HttpStatusCode.BadRequest, accountsWebApiModel);
        //    //    return badResponse;
        //    //}

        //    //ApplicationInitializationBusinessService initializationBusinessService;
        //    //initializationBusinessService = new ApplicationInitializationBusinessService(applicationDataService);
        //    //List<ApplicationMenu> menuItems = initializationBusinessService.GetMenuItems(true, out transaction);

        //    //if (transaction.ReturnStatus == false)
        //    //{
        //    //    accountsWebApiModel.ReturnMessage = transaction.ReturnMessage;
        //    //    accountsWebApiModel.ReturnStatus = transaction.ReturnStatus;
        //    //    var badResponse = Request.CreateResponse<AccountsApiModel>(HttpStatusCode.BadRequest, accountsWebApiModel);
        //    //    return badResponse;
        //    //}

        //    //accountsWebApiModel.ReturnStatus = transaction.ReturnStatus;
        //    //accountsWebApiModel.IsAuthenicated = true;
        //    //accountsWebApiModel.ReturnMessage.Add("Login successful.");
        //    //accountsWebApiModel.MenuItems = menuItems;
        //    //accountsWebApiModel.User = user;

        //    //FormsAuthentication.SetAuthCookie(user.UserId.ToString(), createPersistentCookie: false);


        //    AppUserState appUserState = new AppUserState()
        //    {
        //        Email = user.Email,
        //        UserName = user.UserName,
        //        UserID = user.UserID,
        //        //Theme = user.Theme,
        //        //IsAdmin = user.IsAdmin
        //    };

        //    IdentitySignin(appUserState);

        //    var response = Request.CreateResponse<User>(HttpStatusCode.OK, user);
        //    return response;


        //}


        //[Route("GetUser")]
        //[HttpGet]
        //[WebApiAuthenication]
        //[ValidateModelState]
        //public HttpResponseMessage GetUser()
        //{

        //    Guid userID = new Guid(User.Identity.Name);

        //    AccountsApiModel accountsWebApiModel = new AccountsApiModel();
        //    TransactionalInformation transaction = new TransactionalInformation();
        //    AccountsBusinessService accountsBusinessService;
        //    accountsWebApiModel.IsAuthenicated = true;

        //    accountsBusinessService = new AccountsBusinessService(accountsDataService);
        //    User user = accountsBusinessService.GetUser(userID, out transaction);

        //    transaction.ReturnStatus = true;

        //    if (transaction.ReturnStatus == false)
        //    {
        //        accountsWebApiModel.ReturnMessage = transaction.ReturnMessage;
        //        accountsWebApiModel.ReturnStatus = transaction.ReturnStatus;
        //        accountsWebApiModel.ValidationErrors = transaction.ValidationErrors;
        //        var badResponse = Request.CreateResponse<AccountsApiModel>(HttpStatusCode.BadRequest, accountsWebApiModel);
        //        return badResponse;
        //    }

        //    accountsWebApiModel.ReturnStatus = transaction.ReturnStatus;
        //    accountsWebApiModel.User = user;

        //    var response = Request.CreateResponse<AccountsApiModel>(HttpStatusCode.OK, accountsWebApiModel);
        //    return response;


        //}

        public void IdentitySignin(AppUserState appUserState, string providerKey = null, bool isPersistent = false)
        {
            var claims = new List<Claim>();

            // create *required* claims
            claims.Add(new Claim(ClaimTypes.NameIdentifier, appUserState.UserID.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, appUserState.UserName));
            claims.Add(new Claim(ClaimTypes.Role, appUserState.RoleID.ToString()));
            claims.Add(new Claim("OrganizationID", appUserState.OrganizationID.ToString()));

            // serialized AppUserState object
            claims.Add(new Claim("userState", appUserState.ToString()));

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            // add to user here!
            AuthenticationManager.SignIn(new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = isPersistent,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            }, identity);
        }

        public void IdentitySignout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie,
                DefaultAuthenticationTypes.ExternalCookie);
        }

      


    }
}