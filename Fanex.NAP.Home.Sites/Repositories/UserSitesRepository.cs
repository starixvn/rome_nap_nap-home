// --------------------------------------------------------------------------
// <copyright file="UserSitesRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.31">Created</change>
// </history>

namespace Fanex.NAP.Home.Sites.Repositories
{
    using System.Collections;
    using Fanex.Data;
    using Fanex.NAP.Home.Sites.Criteria.UserSites;
    using Fanex.NAP.Home.Sites.Models.UserSites;

    /// <summary>
    /// User sites repository.
    /// </summary>
    public class UserSitesRepository : IUserSitesRepository
    {
        /// <summary>
        /// Gets all sites of an user.
        /// </summary>
        /// <param name="criteria">The criteria that are use to query data.</param>
        /// <returns>List of UserSiteInfo objects.</returns>
        public UserSiteInfo GetUserSites(GetUserSitesCriteria criteria)
        {
            UserSiteInfo siteData = new UserSiteInfo();
            Hashtable param = criteria.GetSpParams();

            using (IObjectDb db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (GridReader mult = db.QueryMultiple(param))
                {
                    siteData.Sites = mult.Read<SiteInfo>();
                    siteData.SiteUrls = mult.Read<SiteUrlInfo>();
                    siteData.TopMarkedMenus = mult.Read<TopMarkedMenuInfo>();
                }
            }

            return siteData;
        }
    }
}