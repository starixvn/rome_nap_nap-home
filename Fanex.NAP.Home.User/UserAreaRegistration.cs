// --------------------------------------------------------------------------
// <copyright file="UserAreaRegistration.cs">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.22">Create</change>
// </history>

namespace Fanex.NAP.Home.User
{
    using System.Web.Mvc;

    /// <summary>
    /// User login area registration.
    /// </summary>
    public class UserAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "User";
            }
        }

        /// <summary>
        /// Registers an area in an ASP.NET MVC application using the specified area's context information.
        /// </summary>
        /// <param name="context">Encapsulates the information that is required in order to register the area.</param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "User_default",
                "User/{controller}/{action}/{id}",
                new { controller = "Login", action = "Index", id = UrlParameter.Optional });
        }
    }
}