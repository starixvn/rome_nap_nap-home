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
        /// Redirect to Sites page.
        /// </summary>
        /// <returns>Return Sites page.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<UserSiteItem> sites = this._service.QuerytUserSites();

            return View("~/Areas/Sites/Views/UserSites/Index.cshtml", sites);
        }
    }
}