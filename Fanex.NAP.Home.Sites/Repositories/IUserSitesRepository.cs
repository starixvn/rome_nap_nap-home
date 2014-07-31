// --------------------------------------------------------------------------
// <copyright file="IUserSitesRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.31">Created</change>
// </history>

namespace Fanex.NAP.Home.Sites.Repositories
{
    using Fanex.NAP.Home.Sites.Criteria.UserSites;
    using Fanex.NAP.Home.Sites.Models.UserSites;

    public interface IUserSitesRepository
    {
        /// <summary>
        /// Gets all sites of an user.
        /// </summary>
        /// <param name="criteria">The criteria that are use to query data.</param>
        /// <returns>List of UserSiteInfo objects.</returns>
        UserSiteInfo GetUserSites(GetUserSitesCriteria criteria);
    }
}