// --------------------------------------------------------------------------
// <copyright file="SSOController.cs">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.8.12">Created.</change>
// </history>

namespace Fanex.NAP.Home.User.Controllers
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using Fanex.Common;
    using Fanex.NAP.Core.Models;
    using Fanex.NAP.Core.Service.Impl;
    using Fanex.NAP.Core.Service.Interface;
    using Fanex.NAP.Core.Web.Auth;
    using Fanex.NAP.Core.Web.Session;
    using Fanex.NAP.Home.Core.Helpers;
    using NAP.Core.Models.ParameterObject;

    /// <summary>
    /// SSO controller.
    /// </summary>
    public class SSOController : BaseController
    {
        #region Members

        private readonly int _timeExipration = 30;

        private readonly IUserService _userService;
        private readonly ICurrentSession _currentSession;
        private readonly ISecurityService _securityService;

        #endregion Members

        #region Constructors

        public SSOController()
            : this(null, null, null)
        {
        }

        public SSOController(
            IUserService userService,
            ICurrentSession currentSession,
            ISecurityService securityService)
        {
            _userService = userService ?? new UserService();
            _currentSession = currentSession ?? new CurrentSession();
            _securityService = securityService ?? new SecurityService();
        }

        #endregion Constructors

        #region Actions

        /// <summary>
        /// Index page.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="onuserId">The online user ID.</param>
        /// <param name="time">The SSO time.</param>
        /// <returns>Action result.</returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index(string userId, string onuserId, string time)
        {
            string currentTime = _securityService.GetTime();

#pragma warning disable S1226 // Method parameters and caught exceptions should not be reassigned
            userId = Cryptography.CfsDeCrypt(userId.Replace(" ", "+"));
            onuserId = Cryptography.CfsDeCrypt(onuserId.Replace(" ", "+"));
            time = Cryptography.CfsDeCrypt(time.Replace(" ", "+"));
#pragma warning restore S1226 // Method parameters and caught exceptions should not be reassigned
            TimeSpan expiration = Convert.ToDateTime(currentTime) - Convert.ToDateTime(time);

            if (expiration.TotalSeconds <= _timeExipration)
            {
                var ssoLoginParam = new SSOLoginParameter
                {
                    UserId = Convert.ToInt32(userId),
                    SiteId = SiteId,
                    CheckOnUserId = Convert.ToInt32(onuserId),
                    OnUserId = 0,
                    IP = CurrentContext.IP,
                    NetSessionId = Session.SessionID,
                    LoginDate = DateTime.Now,
                    BrowserName = CurrentContext.BrowserName,
                    CountryCode = CurrentContext.CountryCode,
                    Domain = CurrentContext.DomainName,
                    Platform = CurrentContext.Platform
                };

                var onlineUserId =_securityService.SSOLogin(ssoLoginParam);

                if (onlineUserId != 0)
                {
                    var user = _userService.Get(ssoLoginParam.UserId);

                    // Saves user session & auth info.
                    _currentSession.UserId = user.Id;
                    _currentSession.Username = user.Username;
                    _currentSession.OnUserId = onlineUserId;
                    _currentSession.SessionId = Session.SessionID;
                    _currentSession.Level = _userService.GetUserLevel(user.Id, SiteId);
                    CreateAuthInfo(user, HttpContext);

                    return Redirect(FormsAuthentication.DefaultUrl);
                }
            }

            return Redirect(FormsAuthentication.LoginUrl);
        }

        #endregion Actions

        #region Utilities

        /// <summary>
        /// Create authorize information.
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

        #endregion Utilities
    }
}