// --------------------------------------------------------------------------
// <copyright file="UserSitesService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.29">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.NAP.Home.Sites.Criteria.UserSites;
    using Fanex.NAP.Home.Sites.Models.UserSites;
    using Fanex.NAP.Home.Sites.Repositories;
    using Fanex.NAP.Home.Sites.ViewModels.UserSites;

    /// <summary>
    /// User sites service.
    /// </summary>
    public class UserSitesService : SiteBaseService, IUserSitesService
    {
        private IUserSitesRepository _repository;

        public UserSitesService() :
            this(null)
        {
        }

        public UserSitesService(IUserSitesRepository repository)
        {
            this._repository = repository ?? new UserSitesRepository();
        }

        /// <summary>
        /// Gets accessible sites of an user.
        /// </summary>
        /// <param name="filter">The filter object contains User ID and the Environment indicator.</param>
        /// <returns>List of sites that the user can access.</returns>
        public IEnumerable<UserSiteItem> LoadUserSites(UserSitesFilter filter)
        {
            var criteria = new GetUserSitesCriteria
            {
                UserId = filter.UserId,
                Environment = filter.Environment
            };
            UserSiteInfo siteData = this._repository.GetUserSites(criteria);
            IEnumerable<UserSiteItem> result = null;

            if (siteData != null)
            {
                result = GroupUserSitesData(siteData);
            }

            return result;
        }

        /// <summary>
        /// Gets the top favorite functions of a site.
        /// </summary>
        /// <param name="filter">The filter contains necessary conditions to query data.</param>
        /// <returns>List of favorite functions belongs to a site. The number of items is not greater than the passed Top value.</returns>
        public IEnumerable<UserSiteFunctionItem> LoadTopFavoriteFunctionsOfSite(TopFavoriteFunctionsFilter filter)
        {
            IEnumerable<UserSiteFunctionItem> favorites = null;
            IEnumerable<TopMarkedMenuInfo> menus = null;
            var criteria = new GetTopMarkedMenusCriteria
            {
                SiteIds = filter.SiteId.ToString(),
                UserId = filter.UserId,
                Environment = filter.Environment,
                TopSelect = filter.Top
            };

            menus = this.Repository.Fetch<TopMarkedMenuInfo>(criteria);

            if (menus != null && menus.Any())
            {
                favorites = menus.Select(obj => new UserSiteFunctionItem(obj));
            }

            return favorites;
        }

        /// <summary>
        /// Gets the list of functions of a site which logged in user can access.
        /// </summary>
        /// <param name="filter">The filter contains necessary conditions to query data.</param>
        /// <returns>List of user's accessible functions of a site which are organized into hierarchical structure.</returns>
        public IEnumerable<UserSiteFunctionItem> LoadAccessibleFunctionsOfSite(UserSiteFunctionsFilter filter)
        {
            IEnumerable<UserSiteFunctionItem> functions = null;
            IEnumerable<MenuInfo> menus = null;
            var criteria = new GetMenusBySiteUserIdCriteria
            {
                SiteId = filter.SiteId,
                UserId = filter.UserId,
                Environment = filter.Environment
            };

            menus = this.Repository.Fetch<MenuInfo>(criteria);

            if (menus != null && menus.Any())
            {
                functions = RestructureFunctions(menus);
            }

            return functions;
        }

        /// <summary>
        /// Groups the user sites data.
        /// </summary>
        /// <param name="siteData">The site data.</param>
        /// <returns>Data are grouped by sites.</returns>
        private IEnumerable<UserSiteItem> GroupUserSitesData(UserSiteInfo siteData)
        {
            List<UserSiteItem> sites = new List<UserSiteItem>();
            List<UserSiteFunctionItem> favorites = null;
            List<string> domains = null;

            if (siteData.Sites != null && siteData.Sites.Any())
            {
                foreach (SiteInfo site in siteData.Sites)
                {
                    domains = siteData.SiteUrls.Where<SiteUrlInfo>(obj => obj.SiteId == site.SiteId).Select(obj => obj.Link).ToList();
                    favorites = siteData.TopMarkedMenus.Where<TopMarkedMenuInfo>(obj => obj.SiteId == site.SiteId)
                                                       .Select(obj => new UserSiteFunctionItem(obj)).ToList();
                    sites.Add(new UserSiteItem(site, domains, favorites));
                }
            }

            return sites;
        }

        /// <summary>
        /// Restructures the menu data into function tree.
        /// </summary>
        /// <param name="menus">The menus data.</param>
        /// <param name="parentId">Parent ID.</param>
        /// <returns>List of UserSiteFunctionItem indicates the function tree.</returns>
        private IEnumerable<UserSiteFunctionItem> RestructureFunctions(IEnumerable<MenuInfo> menus, int parentId = 0)
        {
            var functions = new List<UserSiteFunctionItem>();
            UserSiteFunctionItem function = null;

            foreach (MenuInfo menu in menus)
            {
                if ((menu.ParentId == null && parentId == 0) || (menu.ParentId == parentId))
                {
                    function = new UserSiteFunctionItem(menu);
                    function.Children.AddRange(RestructureFunctions(menus, menu.MenuItemId));
                    functions.Add(function);
                }
            }

            functions = functions.OrderBy(obj => obj.DisplayOrder).ToList();

            return functions;
        }
    }
}