// --------------------------------------------------------------------------
// <copyright file="BaseController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.8.28">Add properties _currentContext</change>
// </history>

namespace Fanex.NAP.Hub.Areas.UserLogin.Controllers
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using Fanex.NAP.Core.Configuration;
    using Fanex.NAP.Core.Model;
    using Fanex.NAP.Core.Web;
    using Fanex.NAP.Core.Web.Auth;
    using Fanex.NAP.Core.Web.Context;
    using Fanex.NAP.Core.Web.Session;

    /// <summary>
    /// Base controller.
    /// </summary>
    public class BaseController : Controller
    {
        private CurrentContext _currentContext;

        private ICurrentSession _currentSession;

        private bool _enableDirectLogin = NapSettings.Settings.EnableDirectLogin;

        private int _siteId = NapSettings.Settings.SiteId;

        private string _environment = NapSettings.Settings.Environment;

        public BaseController()
        {
            this._currentContext = new CurrentContext();

            ViewFlags = new ViewFlags
            {
                IsReCheckin = NapSettings.Settings.EnableReCheckIn
            };
        }

        /// <summary>
        /// Gets a value indicating whether the current site allows users login directly.
        /// </summary>
        /// <value>0 : users must login through NAP Hub.</value>
        public bool EnableDirectLogin
        {
            get
            {
                return this._enableDirectLogin;
            }
        }

        protected ViewFlags ViewFlags
        {
            get
            {
                return ViewBag.ViewFlags;
            }

            set
            {
                ViewBag.ViewFlags = value;
            }
        }

        protected int SiteId
        {
            get
            {
                return _siteId;
            }
        }

        protected string Environment
        {
            get
            {
                return _environment;
            }
        }

        protected CurrentContext CurrentContext
        {
            get
            {
                return _currentContext;
            }
        }

        protected ICurrentSession CurrentSession
        {
            get
            {
                return _currentSession;
            }

            set
            {
                _currentSession = value;
            }
        }

        /// <summary>
        /// Creates the authentication information.
        /// </summary>
        /// <param name="user">The user object.</param>
        /// <param name="onlineUserId">The online user ID.</param>
        protected void CreateAuthInfo(User user, int onlineUserId)
        {
            var currentDomain = CurrentContext.GetCurrentDomain();

            var authInfo = new AuthInfo()
            {
                UserId = user.Id,
                OnUserId = onlineUserId
            };

            var authTicket = new FormsAuthenticationTicket(
                1,
                user.Username,
                DateTime.Now,
                DateTime.Now.AddMinutes(20000),
                false,
                authInfo.ToXml(),
                currentDomain.Path);

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true,
                Path = currentDomain.Path,
                Name = FormsAuthentication.FormsCookieName
            };

            Response.Cookies.Add(authCookie);
        }

        /// <summary>
        /// Creates the user menu.
        /// </summary>
        /// <param name="user">The user object.</param>
        /// <param name="onlineUserId">The online user ID.</param>
        /// <param name="level">The user level.</param>
        protected void SaveUserSession(User user, int onlineUserId, int level)
        {
            this._currentSession.UserId = user.Id;
            this._currentSession.Username = user.Username;
            this._currentSession.OnUserId = onlineUserId;
            this._currentSession.SessionId = Session.SessionID;
            this._currentSession.Level = level;
        }
    }
}