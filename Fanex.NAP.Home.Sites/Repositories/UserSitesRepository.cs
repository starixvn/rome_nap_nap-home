// --------------------------------------------------------------------------
// <copyright file="UserSitesRepository.cs">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.31">Created</change>
//   <change who="Jason.Ly" date="2014.9.1">Add CheckUserPermissionOnFunction and CheckUserPermissionOnSite</change>
// </history>

namespace Fanex.NAP.Home.Sites.Repositories
{
    using Fanex.Data.Repository;
    using Fanex.NAP.Home.Sites.Criteria.UserSites;
    using Fanex.NAP.Home.Sites.Models;
    using Fanex.NAP.Home.Sites.Models.UserSites;

    /// <summary>
    /// User sites repository.
    /// </summary>
    public class UserSitesRepository : IUserSitesRepository
    {
        private readonly IDynamicRepository dynamicRepository;

        public UserSitesRepository(IDynamicRepository dynamicRepository = null)
            => this.dynamicRepository = dynamicRepository ?? new DynamicRepository();

        /// <summary>
        /// Gets all sites and their menus of an user.
        /// </summary>
        /// <param name="criteria">The criteria that are use to query data.</param>
        /// <returns>SitesInfo object contains a list of site URLs and a list of sites' menus.</returns>
        public SitesInfo GetSitesAndMenus(GetSiteMenusByUserIdCriteria criteria)
        {
            var data = dynamicRepository.FetchMultiple<SiteUrlInfo, MenuInfo>(criteria);

            return new SitesInfo
            {
                SiteUrlList = data.Item1,
                MenuList = data.Item2
            };
        }

        /// <summary>
        /// Checks if an user has permission on a function or not.
        /// </summary>
        /// <param name="criteria">The criteria contain function's information.</param>
        /// <returns>Returns if can access the function or not.</returns>
        public CommandResult CheckUserPermissionOnFunction(CheckPermissionByMenuUserIdCriteria criteria)
        {
            dynamicRepository.Execute(criteria);

            return new CommandResult(criteria.ErrCode, criteria.ErrMsg);
        }

        /// <summary>
        /// Checks if an user has permission on a site or not.
        /// </summary>
        /// <param name="criteria">The criteria contain site's information.</param>
        /// <returns>Returns if can access the site or not.</returns>
        public CommandResult CheckUserPermissionOnSite(CheckUserPermissionBySiteCriteria criteria)
        {
            dynamicRepository.Execute(criteria);

            return new CommandResult(criteria.ErrCode, criteria.ErrMsg);
        }
    }
}