// --------------------------------------------------------------------------
// <copyright file="LoginController.cs">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.22">Create</change>
// </history>

using Fanex.NAP.Core.Service.Impl;
using Fanex.NAP.Core.Service.Interface;

namespace Fanex.NAP.Home.User.Controllers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using Fanex.Common.Extensions;
    using Fanex.NAP.Core.Models;
    using Fanex.NAP.Core.Web;
    using Fanex.NAP.Core.Web.Auth;
    using Fanex.NAP.Core.Web.Session;
    using Fanex.NAP.Home.Core.Helpers;
    using NAP.Core.Configuration;
    using NAP.Core.Models.ParameterObject;

    /// <summary>
    /// Login controller.
    /// </summary>
    public class LoginController : BaseController
    {
        #region Members

        private const string LoginFormToken = "_SecureLoginFormToken";

        private readonly IUserService _userService;
        private readonly IGroupService _groupService;
        private readonly ISiteService _siteService;
        private readonly ICurrentSession _currentSession;
        private readonly ONEbook.UM.Services.IMaintenanceService _maintenanceService;
        private readonly HashAlgorithm _hashAlgorithm;
        private readonly ISecurityService _securityService;

        #endregion Members

        #region Constructors

        public LoginController()
            : this(null, null, null, null, null, null)
        {
        }

        public LoginController(
            IUserService userService,
            IGroupService groupService,
            ISiteService siteService,
            ICurrentSession currentSession,
            ONEbook.UM.Services.IMaintenanceService maintenanceService,
            ISecurityService securityService)
        {
            _userService = userService ?? new UserService();
            _groupService = groupService ?? new GroupService();
            _siteService = siteService ?? new SiteService();
            _currentSession = currentSession ?? new CurrentSession();
            _maintenanceService = maintenanceService ?? new ONEbook.UM.Services.Impl.MaintenanceService();
            _hashAlgorithm = new SHA256Managed();
            _securityService = securityService ?? new SecurityService();
        }

        #endregion Constructors

        #region Properties

        public string FormToken
        {
            get
            {
                if (Session[LoginFormToken] == null)
                {
                    return string.Empty;
                }

                return Session[LoginFormToken].ToString();
            }

            set
            {
                Session[LoginFormToken] = value;
            }
        }

        #endregion Properties

        #region Sign in

        /// <summary>
        /// Redirect to Login page.
        /// </summary>
        /// <returns>Return login page.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            if (_maintenanceService.IsUnderMaintenance())
            {
                return RedirectToAction("Offline", "Common");
            }

            GenerateToken();

            return View();
        }

        /// <summary>
        /// Login indexes page.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="loginToken">The login token.</param>
        /// <returns>Return site.</returns>
        [HttpPost]
        [ValidateInput(false)]
        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed. Suppression is OK here.")]
        public ActionResult Index(string username, string password, string loginToken)
        {
            if (!username.IsNullOrTrimEmpty() && !password.IsNullOrTrimEmpty() && CheckValidToken(loginToken))
            {
                var user = _userService.Get(username.Trim());

                if (user != null)
                {
                    string hashPassword = GetHash(user.Password + loginToken);

                    if (password == hashPassword)
                    {
                        var group = user.Group == null ? null : _groupService.Get(user.Group.Id);
                        var siteLevelList = _userService.GetSiteLevel(user.Id, Environment); // gets all sites of logging-in user

                        ClearToken();

                        if ((group == null || group.IsActive) && siteLevelList.Any(x => x.IsActive))
                        {
                            // check-in user
                            CheckInUser(user);
                            CreateAuthInfo(user, HttpContext);

                            // Check if the login user can access Home or not.
                            if (siteLevelList.Any())
                            {
                                return Redirect(FormsAuthentication.DefaultUrl);
                            }

                            DoSignOut();
                            ViewBag.Message = ViewResources.Your_account_was_suspended;
                        }
                        else
                        {
                            // user account was suspended on all sites
                            ViewBag.Message = siteLevelList.Any() ?
                                ViewResources.Your_account_was_suspended :
                                ViewResources.You_have_no_authorization;
                        }
                    }
                    else
                    {
                        // wrong password
                        ViewBag.Message = ViewResources.Invalid_Username_or_Password;
                    }
                }
                else
                {
                    // user doesn't exist
                    ViewBag.Message = ViewResources.Invalid_Username_or_Password;
                }
            }
            else
            {
                ViewBag.Message = "Invalid input!";
            }

            GenerateToken();

            return View();
        }

        #endregion Sign in

        #region Sign out

        /// <summary>
        /// Logs the out.
        /// </summary>
        /// <returns>Return to login page.</returns>
        public ActionResult LogOut()
        {
            DoSignOut();

            return Redirect(FormsAuthentication.LoginUrl);
        }

        #endregion Sign out

        #region Recheck in

        /// <summary>
        /// Check login.
        /// </summary>
        /// <returns>Return status of login user.</returns>
        [HttpPost]
        public ActionResult RecheckLogin()
        {
            if (_maintenanceService.IsUnderMaintenance())
            {
                return Json("-1");
            }

            // check if not nap hub
            if (SiteId != 0)
            {
                var siteStatus = _siteService.IsActivated(SiteId);

                if (!siteStatus)
                {
                    return Json("1");
                }
            }

            if (_currentSession.OnUserId <= 0)
            {
                return Json("1");
            }

            try
            {
                var isKickedOut = NapSettings.Settings.EnableReCheckIn
                    && _securityService.ReCheckIn(_currentSession.OnUserId, SiteId) > 0;

                return Json(isKickedOut ? "1" : "0");
            }
            catch (Exception)
            {
                return Json("1");
            }
        }

        #endregion Recheck in

        #region Helpers

        /// <summary>
        /// Checks in an user.
        /// </summary>
        /// <param name="user">The user information.</param>
        private void CheckInUser(User user)
        {
            int onlineUserId = 0;
            var loginParameter = new LoginParameter
            {
                UserId = user.Id,
                SiteId = SiteId,
                NetSessionId = Session.SessionID,
                Ip = CurrentContext.IP,
                LoginDate = DateTime.Now,
                BrowserName = CurrentContext.BrowserName,
                CountryCode = CurrentContext.CountryCode,
                Domain = CurrentContext.DomainName,
                Platform = CurrentContext.Platform
            };

            try
            {
                onlineUserId = _securityService.Login(loginParameter);
            }
            catch (Exception ex)
            {
                if (onlineUserId == 0)
                {
                    throw;
                }

                Logging.LogService.Error(ex.Message);
            }

            // saves user session
            _currentSession.Level = _userService.GetUserLevel(user.Id, SiteId);
            _currentSession.UserId = user.Id;
            _currentSession.Username = user.Username;
            _currentSession.OnUserId = onlineUserId;
            _currentSession.SessionId = Session.SessionID;
        }

        /// <summary>
        /// Creates the authentication information.
        /// </summary>
        /// <param name="user">The user object.</param>
        private void CreateAuthInfo(User user, HttpContextBase httpContext)
        {
            var authInfo = new AuthInfo
            {
                UserId = user.Id,
                OnUserId = _currentSession.OnUserId
            };

            var cookiePath = httpContext.Request.ApplicationPath;
            var authTicket = new FormsAuthenticationTicket(
                 1,
                user.Username,
                DateTime.Now,
                DateTime.Now.AddMinutes(20000),
                false,
                authInfo.ToXml(),
                cookiePath);

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true,
                Path = cookiePath,
                Name = FormsAuthentication.FormsCookieName
            };

            Response.Cookies.Add(authCookie);
        }

        /// <summary>
        /// Does the sign out.
        /// </summary>
        private void DoSignOut()
        {
            if (_currentSession.OnUserId > 0)
            {
                _securityService.Logout(_currentSession.OnUserId);
            }

            FormsAuthentication.SignOut();
            _currentSession.ClearAll();
        }

        /// <summary>
        /// Adds the login token to cookie.
        /// </summary>
        /// <param name="loginToken">The login token.</param>
        private void AddTokenToCookie(string loginToken)
        {
            // Create the non-persistent secure form token cookie
            var responseCookie = new HttpCookie(LoginFormToken)
            {
                HttpOnly = true,
                Value = loginToken
            };

            // If we are using SSL, the cookie should be set to secure to
            // prevent it from being sent over HTTP connections
            if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
            {
                responseCookie.Secure = true;
            }

            // Add the cookie to the response
            Response.Cookies.Set(responseCookie);
        }

        /// <summary>
        /// Generates the token for login form.
        /// </summary>
        private void GenerateToken()
        {
            string loginToken = Guid.NewGuid().ToString("N");

            FormToken = loginToken;
            ViewBag.LoginToken = loginToken;
            AddTokenToCookie(loginToken);
        }

        /// <summary>
        /// Checks if the token is valid or not.
        /// </summary>
        /// <param name="loginToken">The login token.</param>
        /// <returns>Boolean value indicates if the login token is valid or not.</returns>
        private bool CheckValidToken(string loginToken)
        {
            return !string.IsNullOrWhiteSpace(loginToken)
                && FormToken == loginToken
                && Request.Cookies[LoginFormToken] != null
                && loginToken == Request.Cookies[LoginFormToken].Value;
        }

        /// <summary>
        /// Gets the hash value of input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>Hash value of input.</returns>
        private string GetHash(string input)
        {
            var encrypted = string.Empty;

            if (!string.IsNullOrWhiteSpace(input))
            {
                var hashData = _hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

                encrypted = BitConverter.ToString(hashData).Replace("-", string.Empty).ToLowerInvariant();
            }

            return encrypted;
        }

        /// <summary>
        /// Clears the token.
        /// </summary>
        private void ClearToken()
        {
            Session.Remove(LoginFormToken);

            if (Request.Cookies[LoginFormToken] != null)
            {
                HttpCookie cookie = new HttpCookie(LoginFormToken);
                cookie.HttpOnly = true;
                cookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(cookie);
            }
        }

        #endregion Helpers
    }
}
