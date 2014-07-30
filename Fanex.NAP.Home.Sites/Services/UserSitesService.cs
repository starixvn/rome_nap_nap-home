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
    using Fanex.NAP.Home.Sites.Configurations;
    using Fanex.NAP.Home.Sites.ViewModels.UserSites;

    /// <summary>
    /// User sites service.
    /// </summary>
    public class UserSitesService : SiteBaseService, IUserSitesService
    {
        /// <summary>
        /// Gets accessible sites of an user.
        /// </summary>
        /// <param name="filter">The filter object contains User ID and the Environment indicator.</param>
        /// <returns>List of sites that the user can access.</returns>
        public IEnumerable<UserSiteItem> LoadUserSites(UserSitesFilter filter)
        {
            this.QueryData(filter);

            return GetSites();
        }

        /// <summary>
        /// Gets the top favorite functions of a site.
        /// </summary>
        /// <param name="filter">The filter contains necessary conditions to query data.</param>
        /// <returns>List of favorite functions belongs to a site. The number of items is not greater than the passed Top value.</returns>
        public IEnumerable<UserSiteFunctionItem> LoadTopFavoriteFunctionsOfSite(TopFavoriteFunctionsFilter filter)
        {
            this.QueryData(filter);

            return GetTopFavoriteFunctions();
        }

        /// <summary>
        /// Gets the list of functions of a site which logged in user can access.
        /// </summary>
        /// <param name="filter">The filter contains necessary conditions to query data.</param>
        /// <returns>List of user's accessible functions of a site which are organized into hierarchical structure.</returns>
        public IEnumerable<UserSiteFunctionItem> LoadAccessibleFunctionsOfSite(UserSiteFunctionsFilter filter)
        {
            this.QueryData(filter);

            return GetFunctions();
        }

        /// <summary>
        /// Creates sample date for User Site page.
        /// </summary>
        /// <returns>Sample data.</returns>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Temperately using.")]
        private List<UserSiteItem> GetSites()
        {
            List<UserSiteItem> sites = new List<UserSiteItem>();
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

            for (int index = 0; index < 10; index++)
            {
                sites.Add(new UserSiteItem
                    {
                        SiteId = siteIds[index],
                        SiteName = siteNames[index],
                        Domains = new List<string>(domainSample),
                        Favorites = GetTopFavoriteFunctions(),
                        Thumbnail = SiteThumbnailsManager.GetImageName(siteIds[index])
                    });
            }

            return sites;
        }

        /// <summary>
        /// Creates sample data for top favorite functions of a site.
        /// </summary>
        /// <returns>Sample data.</returns>
        private List<UserSiteFunctionItem> GetTopFavoriteFunctions()
        {
            List<UserSiteFunctionItem> favorites = new List<UserSiteFunctionItem>();
            string[] favoriteSample = new string[]
            {
                "Daily Win Loss",
                "Virtual Soccer Win Loss Test Long Name",
                "Match Win",
                "Daily Win Loss",
                "Forecast Report"
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

            return favorites;
        }

        /// <summary>
        /// Creates sample date for Functions menu.
        /// </summary>
        /// <returns>Sample data.</returns>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Temperately using.")]
        private List<UserSiteFunctionItem> GetFunctions()
        {
            List<UserSiteFunctionItem> functions = new List<UserSiteFunctionItem>();
            UserSiteFunctionItem subFunction = new UserSiteFunctionItem
            {
                FunctionId = 1,
                FunctionName = "Main Reports"
            };

            subFunction.Children.Add(new UserSiteFunctionItem
            {
                FunctionId = 2,
                FunctionName = "Daily Win Loss (2a)",
                IsFavorite = true,
                Link = "www.google.com"
            });
            subFunction.Children.Add(new UserSiteFunctionItem
            {
                FunctionId = 3,
                FunctionName = "Daily Win Loss (2h)",
                IsFavorite = true,
                Link = "www.google.com"
            });
            subFunction.Children.Add(new UserSiteFunctionItem
            {
                FunctionId = 4,
                FunctionName = "Match Win Loss",
                Link = "www.google.com"
            });
            subFunction.Children.Add(new UserSiteFunctionItem
            {
                FunctionId = 5,
                FunctionName = "Bet Type Win Loss",
                Link = "www.google.com"
            });
            subFunction.Children.Add(new UserSiteFunctionItem
            {
                FunctionId = 6,
                FunctionName = "Forecast Reports",
                IsFavorite = true,
                Link = "www.google.com"
            });

            subFunction.Children[4].Children.Add(new UserSiteFunctionItem
            {
                FunctionId = 7,
                FunctionName = "Bet Type Forecast",
                IsFavorite = true,
                Link = "www.google.com"
            });
            subFunction.Children[4].Children.Add(new UserSiteFunctionItem
            {
                FunctionId = 8,
                FunctionName = "League Forecast",
                Link = "www.google.com"
            });

            functions.Add(subFunction);

            subFunction = new UserSiteFunctionItem
            {
                FunctionId = 9,
                FunctionName = "Win Loss Reports"
            };

            subFunction.Children.Add(new UserSiteFunctionItem
            {
                FunctionId = 10,
                FunctionName = "Parlay Win Loss",
                IsFavorite = true,
                Link = "www.google.com"
            });
            subFunction.Children.Add(new UserSiteFunctionItem
            {
                FunctionId = 11,
                FunctionName = "Bet List",
                Link = "www.google.com"
            });

            subFunction.Children[0].Children.Add(new UserSiteFunctionItem
            {
                FunctionId = 12,
                FunctionName = "Mix Parlay Win Loss",
                IsFavorite = true,
                Link = "www.google.com"
            });
            subFunction.Children[0].Children.Add(new UserSiteFunctionItem
            {
                FunctionId = 8,
                FunctionName = "System Parlay Win Loss",
                IsFavorite = true,
                Link = "www.google.com"
            });

            functions.Add(subFunction);

            return functions;
        }
    }
}