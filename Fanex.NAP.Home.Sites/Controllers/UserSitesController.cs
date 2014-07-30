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
    using Fanex.NAP.Framework.Command;
    using Fanex.NAP.Home.Core.Helpers;
    using Fanex.NAP.Home.Sites.Services;
    using Fanex.NAP.Home.Sites.ViewModels.Common;
    using Fanex.NAP.Home.Sites.ViewModels.UserSites;

    /// <summary>
    /// User sites controller.
    /// </summary>
    [NapSuperAuth]
    public class UserSitesController : BaseController
    {
        private IUserSitesService _service;

        public UserSitesController() :
            this(null)
        {
        }

        public UserSitesController(IUserSitesService service)
        {
            this._service = service ?? new UserSitesService();
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
            filter.Top = 5; // Should get from app settings.
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
            result = this._service.UpdateFavoriteFunction(filter);

            return Json(result);
        }
    }
}