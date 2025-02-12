// --------------------------------------------------------------------------
// <copyright file="UserSitesController.cs">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.22">Create</change>
//   <change who="Jason.Ly" date="2014.9.1">Modify code for change requests.</change>
// </history>

namespace Fanex.NAP.Home.Sites.Controllers
{
    using Fanex.NAP.Core.Service.Impl;
    using Fanex.NAP.Core.Service.Interface;
    using Fanex.NAP.Core.Web.Attribute;
    using Fanex.NAP.Home.Core.Helpers;
    using Fanex.NAP.Home.Sites.Models;
    using Fanex.NAP.Home.Sites.Services;
    using Fanex.NAP.Home.Sites.ViewModels.UserSites;
    using Fanex.Security;
    using System.Linq;
    using System.Web.Mvc;

    /// <summary>
    /// User sites controller.
    /// </summary>
    [NapSuperAuth]
    public class UserSitesController : BaseController
    {
        private const string ForwardLink = "{0}/userlogin/sso?userid={1}&onuserid={2}&time={3}&redirectUrl={4}";

        private readonly IUserSitesService _siteService;
        private readonly ISecurityService _securityService;

        public UserSitesController() :
            this(null, null)
        {
        }

        public UserSitesController(
            IUserSitesService siteService,
            ISecurityService securityService)
        {
            _siteService = siteService ?? new UserSitesService();
            _securityService = securityService ?? new SecurityService();
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
        public ActionResult Sites(string searchFunction)
        {
            var filter = new SearchFunctionsFilter
            {
                UserId = CurrentSession.UserId,
                SearchedFunctionName = searchFunction ?? string.Empty,
                Environment = string.Empty
            };

            var sites = _siteService.LoadUserSitesAndFunctions(filter).ToList();

            ViewBag.SearchedFunctionName = searchFunction;
            ViewBag.IsHomePage = true;

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
            string time = _securityService.GetTime();
            CommandResult checkPermissionResult;
            ForwardResult forwardResult;

            filter.UserId = CurrentSession.UserId;
            filter.Environment = Environment;
            filter.SiteLink = filter.SiteLink ?? string.Empty;
            filter.FunctionLink = string.IsNullOrEmpty(filter.FunctionLink) ? string.Empty : "~" + filter.FunctionLink;

            if (filter.FunctionId.HasValue && filter.FunctionId > 0)
            {
                checkPermissionResult = _siteService.CheckUserPermissionOnFunction(filter);
            }
            else
            {
                checkPermissionResult = _siteService.CheckUserPermissionOnSite(filter);
                filter.FunctionLink = string.Empty;
            }

            forwardResult = new ForwardResult(checkPermissionResult);

            if (checkPermissionResult.IsSuccessful)
            {
                forwardResult.Link = string.Format(
                                         ForwardLink,
                                         filter.SiteLink.Trim(),
                                         Cryptography.CfsEnCrypt(CurrentSession.UserId.ToString()),
                                         Cryptography.CfsEnCrypt(CurrentSession.OnUserId.ToString()),
                                         Cryptography.CfsEnCrypt(time),
                                         filter.FunctionLink);
                forwardResult.Link += $"&{AppSettings.UMToken}={Session[AppSettings.UMToken]}";
            }

            return Json(forwardResult);
        }
    }
}
