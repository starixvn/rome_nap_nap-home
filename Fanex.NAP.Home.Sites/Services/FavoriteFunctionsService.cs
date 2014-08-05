// --------------------------------------------------------------------------
// <copyright file="FavoriteFunctionsService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.8.4">Created</change>
// </history>

namespace Fanex.NAP.Home.Sites.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.NAP.Home.Sites.Criteria.FavoriteFunctions;
    using Fanex.NAP.Home.Sites.Models.FavoriteFunctions;
    using Fanex.NAP.Home.Sites.ViewModels.FavoriteFunctions;

    /// <summary>
    /// Favorite functions service.
    /// </summary>
    public class FavoriteFunctionsService : SiteBaseService, IFavoriteFunctionsService
    {
        /// <summary>
        /// Gets the marked functions.
        /// </summary>
        /// <param name="filter">The object contains filter conditions.</param>
        /// <returns>Favorite functions grouped by site IDs and site names.</returns>
        public IEnumerable<SiteItem> LoadMarkedFunctions(GettingFavoriteFunctionsFilter filter)
        {
            var criteria = new GetMarkedMenusCriteria
            {
                UserId = filter.UserId,
                Environment = filter.Environment,
                PageSize = filter.PageSize,
                PageIndex = filter.PageIndex - 1
            };
            List<SiteItem> markedFunction = new List<SiteItem>();
            IEnumerable<MarkedMenuInfo> markedMenus = null;

            if (criteria.IsValid())
            {
                markedMenus = this.Repository.Fetch<MarkedMenuInfo>(criteria);
                filter.TotalItemCount = criteria.TotalItemCount;
            }

            if (markedMenus != null && markedMenus.Any())
            {
                IEnumerable<IGrouping<int?, MarkedMenuInfo>> menuGroups = markedMenus.GroupBy(obj => obj.SiteId);
                IEnumerable<FavoriteFunctionItem> favorites = null;
                MarkedMenuInfo firstMenu = null;
                SiteItem siteItem = null;

                foreach (IGrouping<int?, MarkedMenuInfo> group in menuGroups)
                {
                    firstMenu = group.ElementAt<MarkedMenuInfo>(0);
                    favorites = group.Select(obj => new FavoriteFunctionItem(obj));
                    siteItem = new SiteItem(firstMenu, favorites);
                    markedFunction.Add(siteItem);
                }
            }

            return markedFunction;
        }
    }
}