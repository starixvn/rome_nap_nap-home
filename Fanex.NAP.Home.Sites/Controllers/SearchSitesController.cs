// --------------------------------------------------------------------------
// <copyright file="SearchSitesController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.28">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.Controllers
{
    using System.Web.Mvc;
    using Fanex.NAP.Core.Web.Attribute;
    using Fanex.NAP.Home.Core.Helpers;

    /// <summary>
    /// User sites controller.
    /// </summary>
    [NapSuperAuth]
    public class SearchSitesController : BaseController
    {
        /// <summary>
        /// Redirect to Login page.
        /// </summary>
        /// <param name="functionName">Searched function name.</param>
        /// <param name="pageIndex">Current page index.</param>
        /// <returns>Return login page.</returns>
        [HttpGet]
        public ActionResult Index(string functionName, int pageIndex = 0)
        {
            ViewBag.SearchedFunctionName = functionName;
            ViewBag.CurrentPageIndex = pageIndex;

            return View();
        }
    }
}