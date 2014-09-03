// --------------------------------------------------------------------------
// <copyright file="UserSitesController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.22">Create</change>
//   <change who="Jason.Ly" date="2014.9.1">Modify code for change requests.</change>
// </history>

namespace Fanex.NAP.Home.Sites.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Fanex.NAP.Core.Service.Impl;
    using Fanex.NAP.Core.Service.Interface;
    using Fanex.NAP.Core.Web.Attribute;
    using Fanex.NAP.Framework.Command;
    using Fanex.NAP.Home.Core.Helpers;
    using Fanex.NAP.Home.Sites.Services;
    using Fanex.NAP.Home.Sites.ViewModels.UserSites;
    using Fanex.Security;

    /// <summary>
    /// User sites controller.
    /// </summary>
    [NapSuperAuth]
    public class UserSitesController : BaseController
    {
        private const string CachedDefaultSiteLinks = "Fanex.NAP.Home.DefaultSiteLinks";
        private const string ForwardLink = "{0}/userlogin/sso?userid={1}&onuserid={2}&time={3}&redirectUrl={4}";

        private IUserSitesService _siteService;
        private IUserService _userService;

        public UserSitesController() :
            this(null, null)
        {
        }

        public UserSitesController(IUserSitesService siteService, IUserService userService)
        {
            this._siteService = siteService ?? new UserSitesService();
            this._userService = userService ?? new UserService();
        }

        /// <summary>
        /// Default page of MVC project.
        /// </summary>
        /// <returns>Redirect to Site page.</returns>
        public ActionResult Index()
        {
            return RedirectToAction("Sites", "UserSites", new { area = "Sites" });
        }

        /// <summary>
        /// Redirect to Sites page.
        /// </summary>
        /// <param name="searchFunction">Searched function name.</param>
        /// <returns>Return Sites page.</returns>
        [HttpGet]
        public ActionResult Sites(string searchFunction = null)
        {
            var filter = new SearchFunctionsFilter
            {
                UserId = this.CurrentSession.UserId,
                SearchedFunctionName = searchFunction ?? string.Empty,
                Environment = this.Environment
            };
            IEnumerable<SiteItem> sites = this._siteService.LoadUserSitesAndFunctions(filter);

            ViewBag.SearchedFunctionName = searchFunction;

            return View(sites);
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

            if (filter.FunctionId.HasValue && filter.FunctionId > 0)
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