// --------------------------------------------------------------------------
// <copyright file="ErrorController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.31">Created</change>
// </history>

namespace Fanex.NAP.Home.User.Controllers
{
    using System.Web.Mvc;
    using System.Web.Security;
    using Fanex.NAP.Home.Core.Helpers;

    /// <summary>
    /// Error controller.
    /// </summary>
    public class ErrorController : BaseController
    {
        /// <summary>
        /// Redirect to default error page.
        /// </summary>
        /// <returns>Access Denied view.</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Redirect to access denied page.
        /// </summary>
        /// <returns>Access Denied view.</returns>
        public ActionResult AccessDenied()
        {
            return View();
        }

        /// <summary>
        /// Redirect to not found page.
        /// </summary>
        /// <returns>Access Denied view.</returns>
        public ActionResult NotFound()
        {
            if (this.CurrentSession.UserId == 0)
            {
                return this.Redirect(FormsAuthentication.LoginUrl);
            }

            return View();
        }
    }
}