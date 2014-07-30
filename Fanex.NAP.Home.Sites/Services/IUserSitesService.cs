// --------------------------------------------------------------------------
// <copyright file="IUserSitesService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.29">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.Services
{
    using System.Collections.Generic;
    using Fanex.NAP.Home.Sites.ViewModels.UserSites;

    public interface IUserSitesService : ISiteBaseService
    {
        /// <summary>
        /// Gets accessible sites of an user.
        /// </summary>
        /// <param name="filter">The filter object contains User ID and the Environment indicator.</param>
        /// <returns>List of sites that the user can access.</returns>
        IEnumerable<UserSiteItem> LoadUserSites(UserSitesFilter filter);

        /// <summary>
        /// Gets the top favorite functions of a site.
        /// </summary>
        /// <param name="filter">The filter contains necessary conditions to query data.</param>
        /// <returns>List of favorite functions belongs to a site. The number of items is not greater than the passed Top value.</returns>
        IEnumerable<UserSiteFunctionItem> LoadTopFavoriteFunctionsOfSite(TopFavoriteFunctionsFilter filter);

        /// <summary>
        /// Gets the list of functions of a site which logged in user can access.
        /// </summary>
        /// <param name="filter">The filter contains necessary conditions to query data.</param>
        /// <returns>List of user's accessible functions of a site which are organized into hierarchical structure.</returns>
        IEnumerable<UserSiteFunctionItem> LoadAccessibleFunctionsOfSite(UserSiteFunctionsFilter filter);
    }
}