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
        /// Forwards the specified site.
        /// </summary>
        /// <param name="siteLink">The URL of site.</param>
        /// <param name="functionLink">The redirected URL after SSO.</param>
        /// <returns>Return to selected site.</returns>
        [HttpGet]
        public ActionResult Forward(string siteLink, string functionLink = null)
        {
            string time = this._userService.UserSSOGetTime();

            siteLink = siteLink ?? string.Empty;
            functionLink = functionLink ?? string.Empty;

            return new RedirectResult(
                string.Format(
                    "{0}/userlogin/sso?userid={1}&onuserid={2}&time={3}&redirectUrl=~{4}",
                    siteLink.Trim(),
                    Cryptography.CfsEnCrypt(this.CurrentSession.UserId.ToString()),
                    Cryptography.CfsEnCrypt(this.CurrentSession.OnUserId.ToString()),
                    Cryptography.CfsEnCrypt(time),
                    functionLink));
        }
    }
}