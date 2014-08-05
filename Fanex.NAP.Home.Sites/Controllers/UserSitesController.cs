// --------------------------------------------------------------------------
// <copyright file="UserSitesController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.22">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Fanex.NAP.Core.Web.Attribute;
    using Fanex.NAP.Home.Core.Helpers;
    using Fanex.NAP.Home.Sites.Services;
    using Fanex.NAP.Home.Sites.ViewModels.UserSites;

    /// <summary>
    /// User sites controller.
    /// </summary>
    [NapSuperAuth]
    public class UserSitesController : SiteBaseController
    {
        private const string CachedDefaultSiteLinks = "Fanex.NAP.Home.DefaultSiteLinks";

        private IUserSitesService _service;

        public UserSitesController() :
            this(null)
        {
        }

        public UserSitesController(IUserSitesService service)
        {
            this._service = service ?? new UserSitesService();
            ViewBag.DefaultLinks = SessionManager.Get<Dictionary<int, string>>(CachedDefaultSiteLinks);
            ViewBag.IsHomePage = true;
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
        /// <returns>Return Sites page.</returns>
        [HttpGet]
        public ActionResult Sites()
        {
            var filter = new UserSitesFilter
            {
                UserId = this.CurrentSession.UserId,
                Environment = this.Environment
            };
            IEnumerable<UserSiteItem> sites = this._service.LoadUserSites(filter);
            Dictionary<int, string> defaultLinks = new Dictionary<int, string>();

            if (sites != null)
            {
                foreach (UserSiteItem site in sites)
                {
                    defaultLinks.Add(site.SiteId, site.DefaultLink);
                }

                SessionManager.Set<Dictionary<int, string>>(CachedDefaultSiteLinks, defaultLinks);
                ViewBag.DefaultLinks = defaultLinks;
            }

            return View(sites);
        }

        /// <summary>
        /// Gets the partial view of top favorite functions of a site.
        /// </summary>
        /// <param name="filter">The filter contains necessary conditions to query data.</param>
        /// <returns>Returns top favorite functions.</returns>
        [HttpPost]
        public PartialViewResult TopFavoriteFunctions(TopFavoriteFunctionsFilter filter)
        {
            IEnumerable<UserSiteFunctionItem> functions = null;

            filter.UserId = this.CurrentSession.UserId;
            filter.Environment = this.Environment;
            filter.Top = AppSettings.TopFavorites;
            functions = this._service.LoadTopFavoriteFunctionsOfSite(filter);

            return PartialView(functions);
        }

        /// <summary>
        /// Gets the partial view of user's accessible functions which organized into tree view.
        /// </summary>
        /// <param name="filter">The filter contains necessary conditions to query data.</param>
        /// <returns>Returns function list.</returns>
        [HttpPost]
        public PartialViewResult FunctionList(UserSiteFunctionsFilter filter)
        {
            IEnumerable<UserSiteFunctionItem> functions = null;

            filter.UserId = this.CurrentSession.UserId;
            filter.Environment = this.Environment;
            functions = this._service.LoadAccessibleFunctionsOfSite(filter);

            return PartialView(functions);
        }
    }
}