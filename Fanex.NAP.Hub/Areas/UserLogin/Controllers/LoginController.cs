// --------------------------------------------------------------------------
// <copyright file="LoginController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.8.28">Add properties _currentContext</change>
//   <change who="Jason.Ly" date="2014.7.16">Encrypt password.</change>
// </history>

namespace Fanex.NAP.Hub.Areas.UserLogin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using Fanex.Common.Extensions;
    using Fanex.NAP.Core.Criteria.User;
    using Fanex.NAP.Core.Model;
    using Fanex.NAP.Core.Model.ValueObject;
    using Fanex.NAP.Core.Service.Impl;
    using Fanex.NAP.Core.Service.Interface;
    using Fanex.NAP.Core.Web;
    using Fanex.NAP.Core.Web.Session;

    /// <summary>
    /// Login controller.
    /// </summary>
    public class LoginController : BaseController
    {
        #region Members

        private const string LoginFormToken = "_SecureLoginFormToken";

        private IUserService _userService;
        private IGroupService _groupService;
        private ISiteService _siteService;
        private IMenuItemService _menuItemService;
        private IMaintenanceService _maintenanceService;
        private HashAlgorithm _hashAlgorithm;

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
            IMenuItemService menuItemService,
            ICurrentSession currentSession,
            IMaintenanceService maintenanceService)
        {
            this._userService = userService ?? new UserService();
            this._groupService = groupService ?? new GroupService();
            this._siteService = siteService ?? new SiteService();
            this._menuItemService = menuItemService ?? new MenuItemService();
            this.CurrentSession = currentSession ?? new CurrentSession();
            this._maintenanceService = maintenanceService ?? new MaintenanceService();
            this._hashAlgorithm = new SHA256Managed();
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
        /// Login indexes page.
        /// </summary>
        /// <returns>Return login page.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            Downtime maintenanceInfo = this._maintenanceService.GetDowntimeInfo();

            if (maintenanceInfo.IsDowntime)
            {
                return this.RedirectToAction("Offline", "Common");
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
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed. Suppression is OK here.")]
        public ActionResult Index(string username, string password, string loginToken)
        {
            if (!username.IsNullOrTrimEmpty() && !password.IsNullOrTrimEmpty() && CheckValidToken(loginToken))
            {
                var user = this._userService.GetByUsername(username.Trim()) as User;

                if (user != null)
                {
                    string hashPassword = GetHash(user.Password + loginToken);

                    if (password == hashPassword)
                    {
                        var group = user.Group == null ? null : this._groupService.GetById(user.Group.Id);
                        var siteLevelList = this._siteService.GetSiteLevelByUser(user.Id, this.Environment); // gets all sites of logging-in user

                        ClearToken();

                        if ((group == null || group.IsActive) && siteLevelList.Where(x => x.IsActive).Any())
                        {
                            // check-in user
                            var criteria = new UserCheckInUserCriteria
                            {
                                UserId = user.Id,
                                SiteId = this.SiteId,
                                NetSessionId = Session.SessionID,
                                Ip = CurrentContext.IP,
                                LoginDate = DateTime.Now,
                                BrowserName = CurrentContext.BrowserName,
                                CountryCode = CurrentContext.CountryCode,
                                Domain = CurrentContext.DomainName,
                                Platform = CurrentContext.Platform
                            };

                            this._userService.UserCheckIn(ref criteria);

                            int level = this._userService.GetUserSiteLevel(user.Id, this.SiteId);

                            SaveUserSession(user, criteria.OnUserId, level);

                            // current site is not NAP Hub
                            if (this.SiteId != 0)
                            {
                                CreateUserMenu(user.Id);
                            }

                            CreateAuthInfo(user, criteria.OnUserId);

                            // if the user login directly, not through NAP Hub
                            if (this.EnableDirectLogin)
                            {
                                // checks if logging-in user is active on this site
                                if (siteLevelList.Where(x => x.Site.Id == this.SiteId && x.IsActive).Any())
                                {
                                    return Redirect(FormsAuthentication.DefaultUrl);
                                }

                                DoSignOut();
                                ViewBag.Message = ViewResources.Your_account_was_suspended;
                            }
                            else
                            {
                                // user has access to multiple sites
                                if (siteLevelList.Where(s => s.IsActive).Count() > 1)
                                {
                                    return this.RedirectToAction("Site", "UserSite");
                                }

                                // user has access to one site
                                Site site = siteLevelList.Where(s => s.IsActive).First().Site;

                                List<SiteUrl> listSiteUrl = this._siteService.GetSiteUrlBySiteId(site.Id, this.Environment).Where(u => u.IsActive).ToList();

                                if (listSiteUrl.Count() > 1)
                                {
                                    return this.RedirectToAction("Site", "UserSite");
                                }

                                return this.RedirectToAction("Forward", "UserSite", new { id = listSiteUrl.First().SiteUrlId });
                            }
                        }
                        else
                        {
                            // user account was suspended on all sites
                            if (siteLevelList.Any())
                            {
                                ViewBag.Message = ViewResources.Your_account_was_suspended;
                            }
                            else
                            {
                                // logging-in user belongs to no site
                                ViewBag.Message = ViewResources.You_have_no_authorization;
                            }
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

            return this.View();
        }

        #endregion Sign in

        #region Sign out

        /// <summary>
        /// Logs the out.
        /// </summary>
        /// <returns>Return to login page.</returns>
        public ActionResult LogOut()
        {
            this.DoSignOut();

            return this.Redirect(FormsAuthentication.LoginUrl);
        }

        #endregion Sign out

        #region Recheck in

        /// <summary>
        /// Check login.
        /// </summary>
        /// <returns>Return status of login user.</returns>
        [HttpPost]
        public ActionResult ReCheckLogin()
        {
            Downtime maintenanceInfo = this._maintenanceService.GetDowntimeInfo();

            if (maintenanceInfo.IsDowntime)
            {
                return this.Json("-1");
            }

            // check if not nap hub
            if (this.SiteId != 0)
            {
                bool siteStatus = this._siteService.GetStatusById(this.SiteId);

                if (!siteStatus)
                {
                    return this.Json("1");
                }
            }

            var criteria = new UserReCheckInUserCriteria()
            {
                OnUserId = Convert.ToInt32(this.CurrentSession.OnUserId)
            };

            this._userService.ReUserCheckIn(ref criteria);

            return this.Json(criteria.IsKickedOut ? "1" : "0");
        }

        #endregion Recheck in

        #region Helpers

        /// <summary>
        /// Creates the user menu.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        private void CreateUserMenu(int userId)
        {
            this.CurrentSession.MenuItemList = (IList<MenuItem>)this._menuItemService.GetByUserAndSiteIncludeGroup(
                userId,
                this.SiteId,
                this.Environment,
                true);
        }

        /// <summary>
        /// Does the sign out.
        /// </summary>
        private void DoSignOut()
        {
            var criteria = new LogOutUserCriteria
            {
                OnUserId = this.CurrentSession.OnUserId
            };

            this._userService.UserLogOut(criteria);
            FormsAuthentication.SignOut();
            this.CurrentSession.ClearAll();
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

                encrypted = BitConverter.ToString(hashData).Replace("-", string.Empty).ToLower();
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
                var cookie = new HttpCookie(LoginFormToken);

                cookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(cookie);
            }
        }

        #endregion Helpers
    }
}