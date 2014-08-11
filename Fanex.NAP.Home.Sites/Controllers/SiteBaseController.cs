// --------------------------------------------------------------------------
// <copyright file="SiteBaseController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.8.1">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.Controllers
{
    using System.Web.Mvc;
    using Fanex.NAP.Core.Service.Impl;
    using Fanex.NAP.Core.Service.Interface;
    using Fanex.NAP.Framework.Command;
    using Fanex.NAP.Home.Core.Helpers;
    using Fanex.NAP.Home.Sites.Services;
    using Fanex.NAP.Home.Sites.ViewModels.Common;
    using Fanex.Security;

    /// <summary>
    /// Site base controller.
    /// </summary>
    public class SiteBaseController : BaseController
    {
        private const string ForwardLink = "{0}/userlogin/sso?userid={1}&onuserid={2}&time={3}&redirectUrl={4}";

        private ISiteBaseService _siteService;
        private IUserService _userService;

        public SiteBaseController() :
            this(null, null)
        {
        }

        public SiteBaseController(ISiteBaseService siteService, IUserService userService)
        {
            this._siteService = siteService ?? new SiteBaseService();
            this._userService = userService ?? new UserService();
        }

        /// <summary>
        /// Marks or unmarks a function is favorite or not.
        /// </summary>
        /// <param name="filter">Filter objects that is used to identify which function will be marked or unmarked.</param>
        /// <returns>JSON data indicate the updating process is successful or not.</returns>
        [HttpPost]
        public JsonResult UpdatingFavoriteFunction(UpdatingFavoriteFunctionFilter filter)
        {
            ICommandResult result = null;

            filter.UserId = this.CurrentSession.UserId;
            result = this._siteService.UpdateFavoriteFunction(filter);

            return Json(result);
        }

        /// <summary>
        /// Forwards the specified site or function.
        /// </summary>
        /// <param name="filter">The object contains the links of site and function that need to redirect to.</param>
        /// <returns>Return the URL of selected site or function.</returns>
        [HttpPost]
        public JsonResult Forward(ForwardFilter filter)
        {
            string time = this._userService.UserSSOGetTime();
            ICommandResult checkPermissionResult = null;
            ForwardResult forwardResult = null;

            filter.UserId = this.CurrentSession.UserId;
            filter.Environment = this.Environment;
            filter.SiteLink = filter.SiteLink ?? string.Empty;
            filter.FunctionLink = string.IsNullOrEmpty(filter.FunctionLink) ? string.Empty : "~" + filter.FunctionLink;

            if (filter.FunctionId.HasValue)
            {
                checkPermissionResult = this._siteService.CheckUserPermissionOnFunction(filter);
            }
            else
            {
                checkPermissionResult = this._siteService.CheckUserPermissionOnSite(filter);
            }

            forwardResult = new ForwardResult(checkPermissionResult);

            if (checkPermissionResult.IsSuccessful)
            {
                forwardResult.Link = string.Format(
                                         ForwardLink,
                                         filter.SiteLink.Trim(),
                                         Cryptography.CfsEnCrypt(this.CurrentSession.UserId.ToString()),
                                         Cryptography.CfsEnCrypt(this.CurrentSession.OnUserId.ToString()),
                                         Cryptography.CfsEnCrypt(time),
                                         filter.FunctionLink);
            }

            return Json(forwardResult);
        }
    }
}