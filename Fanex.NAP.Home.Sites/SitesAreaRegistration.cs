// --------------------------------------------------------------------------
// <copyright file="SitesAreaRegistration.cs">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.22">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites
{
    using System.Web.Mvc;

    /// <summary>
    /// Site area registration.
    /// </summary>
    public class SitesAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Sites";
            }
        }

        /// <summary>
        /// Registers an area in an ASP.NET MVC application using the specified area's context information.
        /// </summary>
        /// <param name="context">Encapsulates the information that is required in order to register the area.</param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Sites_default",
                "Sites/{controller}/{action}/{id}",
                new { controller = "UserSites", action = "Index", id = UrlParameter.Optional });
        }
    }
}