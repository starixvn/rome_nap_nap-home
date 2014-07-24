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
    using System.Web.Mvc;
    using Fanex.NAP.Core.Web.Attribute;

    /// <summary>
    /// Base controller.
    /// </summary>
    [NapSuperAuth]
    public class UserSitesController : Controller
    {
        /// <summary>
        /// Redirect to Login page.
        /// </summary>
        /// <returns>Return login page.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Areas/Sites/Views/UserSites/Index.cshtml");
        }
    }
}