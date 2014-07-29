// --------------------------------------------------------------------------
// <copyright file="FavoriteFunctionsController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.29">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.Controllers
{
    using System.Web.Mvc;
    using Fanex.NAP.Home.Core.Helpers;

    /// <summary>
    /// Favorite functions controller.
    /// </summary>
    public class FavoriteFunctionsController : BaseController
    {
        /// <summary>
        /// Redirect to Favorite Functions page.
        /// </summary>
        /// <returns>Return Favorite Functions page.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}