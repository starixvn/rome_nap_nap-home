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
    using System.Diagnostics.CodeAnalysis;
    using Fanex.NAP.Home.Core.Helpers;
    using Fanex.NAP.Home.Sites.Configurations;
    using Fanex.NAP.Home.Sites.ViewModels.UserSites;

    /// <summary>
    /// User sites service.
    /// </summary>
    public class UserSitesService : BaseService, IUserSitesService
    {
        /// <summary>
        /// Gets accessible sites of an user.
        /// </summary>
        /// <returns>List of sites that the user can access.</returns>
        public IEnumerable<UserSiteItem> QuerytUserSites()
        {
            return GetSites();
        }

        /// <summary>
        /// Creates sample date for User Site page.
        /// </summary>
        /// <returns>Sample data.</returns>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed. Suppression is OK here.")]
        private IEnumerable<UserSiteItem> GetSites()
        {
            List<UserSiteItem> sites = new List<UserSiteItem>();
            List<UserSiteFunctionItem> favorites = new List<UserSiteFunctionItem>();
            int[] siteIds = new int[] { 1, 3, 12, 15, 67, 70, 77, 83, 84, 85 };
            string[] siteNames = new string[]
            {
                "NAP Admin",
                "NAP Accounting Report",
                "mSite",
                "NAP DW",
                "NAP Settlement",
                "NAP Special Delete",
                "NAP Live Charts",
                "NAP CRM",
                "NAP Customer Management",
                "NAP TGRS"
            };
            string[] favoriteSample = new string[]
            {
                "Daily Win Loss",
                "Virtual Soccer Win Loss Test Long Name",
                "Match Win",
                "Daily Win Loss",
                "Forecast Report"
            };
            string[] domainSample = new string[]
            {
                "http://10.18.200.209:8007/nap-sc-pro-long-link",
                "http://10.18.200.209:8007/nap-sc-pro-long-link",
                "http://10.18.200.209:8007/nap-sc-pro-long-link",
                "http://10.18.200.209:8007/nap-sc-pro-long-link",
                "http://10.18.200.209:8007/nap-sc-pro-long-link",
                "http://10.18.200.209:8007/nap-sc-pro-long-link",
                "http://10.18.200.209:8007/nap-sc-pro-long-link",
                "http://10.18.200.209:8007/nap-sc-pro-long-link",
                "http://10.18.200.209:8007/nap-sc-pro-long-link",
                "http://10.18.200.209:8007/nap-sc-pro-long-link"
            };

            foreach (string fav in favoriteSample)
            {
                favorites.Add(new UserSiteFunctionItem
                    {
                        FunctionId = 0,
                        FunctionName = fav,
                        IsFavorite = true,
                        Link = "http://10.18.200.209:8007/nap-sc-pro-long-link"
                    });
            }

            for (int index = 0; index < 10; index++)
            {
                sites.Add(new UserSiteItem
                    {
                        SiteId = siteIds[index],
                        SiteName = siteNames[index],
                        Domains = new List<string>(domainSample),
                        Favorites = favorites,
                        Thumbnail = SiteThumbnailsManager.GetImageName(siteIds[index])
                    });
            }

            return sites;
        }
    }
}