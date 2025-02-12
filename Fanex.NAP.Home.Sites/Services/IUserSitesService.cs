// --------------------------------------------------------------------------
// <copyright file="IUserSitesService.cs">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.29">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.Services
{
    using System.Collections.Generic;
    using Fanex.NAP.Home.Sites.Models;
    using Fanex.NAP.Home.Sites.ViewModels.UserSites;

    public interface IUserSitesService
    {
        /// <summary>
        /// Gets accessible sites and functions of an user.
        /// </summary>
        /// <param name="filter">The filter object contains User ID and the Environment indicator.</param>
        /// <returns>List of sites and sites' functions that the user can access.</returns>
        IEnumerable<SiteItem> LoadUserSitesAndFunctions(SearchFunctionsFilter filter);

        /// <summary>
        /// Checks if an user has permission on a function or not.
        /// </summary>
        /// <param name="filter">The filter contains function's information.</param>
        /// <returns>Returns if can access the function or not.</returns>
        CommandResult CheckUserPermissionOnFunction(ForwardFilter filter);

        /// <summary>
        /// Checks if an user has permission on a site or not.
        /// </summary>
        /// <param name="filter">The filter contains site's information.</param>
        /// <returns>Returns if can access the site or not.</returns>
        CommandResult CheckUserPermissionOnSite(ForwardFilter filter);
    }
}