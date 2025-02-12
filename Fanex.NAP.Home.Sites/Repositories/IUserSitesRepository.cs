// --------------------------------------------------------------------------
// <copyright file="IUserSitesRepository.cs">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.31">Created</change>
//   <change who="Jason.Ly" date="2014.9.1">Add CheckUserPermissionOnFunction and CheckUserPermissionOnSite</change>
// </history>

namespace Fanex.NAP.Home.Sites.Repositories
{
    using Fanex.NAP.Home.Sites.Criteria.UserSites;
    using Fanex.NAP.Home.Sites.Models;
    using Fanex.NAP.Home.Sites.Models.UserSites;

    public interface IUserSitesRepository
    {
        /// <summary>
        /// Gets all sites and their menus of an user.
        /// </summary>
        /// <param name="criteria">The criteria that are use to query data.</param>
        /// <returns>SitesInfo object contains a list of site URLs and a list of sites' menus.</returns>
        SitesInfo GetSitesAndMenus(GetSiteMenusByUserIdCriteria criteria);

        /// <summary>
        /// Checks if an user has permission on a function or not.
        /// </summary>
        /// <param name="criteria">The criteria contain function's information.</param>
        /// <returns>Returns if can access the function or not.</returns>
        CommandResult CheckUserPermissionOnFunction(CheckPermissionByMenuUserIdCriteria criteria);

        /// <summary>
        /// Checks if an user has permission on a site or not.
        /// </summary>
        /// <param name="criteria">The criteria contain site's information.</param>
        /// <returns>Returns if can access the site or not.</returns>
        CommandResult CheckUserPermissionOnSite(CheckUserPermissionBySiteCriteria criteria);
    }
}