// --------------------------------------------------------------------------
// <copyright file="SSOController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.8.28">Add properties _currentContext</change>
// </history>

namespace Fanex.NAP.Hub.Areas.UserLogin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Mvc;
    using System.Web.Security;
    using Fanex.Common;
    using Fanex.NAP.Core.Criteria.User;
    using Fanex.NAP.Core.Model;
    using Fanex.NAP.Core.Service.Impl;
    using Fanex.NAP.Core.Service.Interface;
    using Fanex.NAP.Core.Web.Session;

    /// <summary>
    /// SSO controller.
    /// </summary>
    public class SSOController : BaseController
    {
        #region members

        private const int TimeExpiration = 30;

        private IUserService _userService;
        private IMenuItemService _menuItemService;

        #endregion

        #region ctors

        public SSOController()
            : this(null, null, null)
        {
        }

        public SSOController(
            IUserService userService,
            IMenuItemService menuItemService,
            ICurrentSession currentSession)
        {
            this._userService = userService ?? new UserService();
            this._menuItemService = menuItemService ?? new MenuItemService();
            this.CurrentSession = currentSession ?? new CurrentSession();
        }

        #endregion

        /// <summary>
        /// Indexes the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="onuserId">The on user identifier.</param>
        /// <param name="time">The time expired.</param>
        /// <returns>Return to site.</returns>
        [AllowAnonymous]
        [HttpGet]
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed. Suppression is OK here.")]
        public ActionResult Index(string userId, string onuserId, string time)
        {
            userId = Cryptography.CfsDeCrypt(userId.Replace(" ", "+"));
            onuserId = Cryptography.CfsDeCrypt(onuserId.Replace(" ", "+"));
            time = Cryptography.CfsDeCrypt(time.Replace(" ", "+"));
            string currentTime = this._userService.UserSSOGetTime();

            TimeSpan expiration = Convert.ToDateTime(currentTime) - Convert.ToDateTime(time);

            if (expiration.TotalSeconds <= TimeExpiration)
            {
                // do SSO check-in
                var criteria = new UserSSOCheckInUserCriteria
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

                this._userService.UserSSOCheckIn(ref criteria);

                if (criteria.OnUserId != 0)
                {
                    var user = this._userService.GetById(criteria.UserId) as User;
                    int level = this._userService.GetUserSiteLevel(user.Id, this.SiteId);

                    SaveUserSession(user, criteria.OnUserId, level);

                    if (this.SiteId != 0)
                    {
                        CreateUserMenu(user.Id);
                    }

                    CreateAuthInfo(user, criteria.OnUserId);

                    return this.Redirect(FormsAuthentication.DefaultUrl);
                }
            }

            return this.Redirect(FormsAuthentication.LoginUrl);
        }

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
    }
}