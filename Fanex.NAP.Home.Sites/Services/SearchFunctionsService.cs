// --------------------------------------------------------------------------
// <copyright file="SearchFunctionsService.cs" company="Nexcel Solutions Vietnam">
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
    using Fanex.NAP.Home.Sites.Criteria.SearchFunctions;
    using Fanex.NAP.Home.Sites.Models.SearchFunctions;
    using Fanex.NAP.Home.Sites.ViewModels.SearchFunctions;

    /// <summary>
    /// User sites service.
    /// </summary>
    public class SearchFunctionsService : SiteBaseService, ISearchFunctionsService
    {
        /// <summary>
        /// Gets all functions which satisfy search conditions.
        /// </summary>
        /// <param name="filter">The filter object contains searching conditions.</param>
        /// <returns>List of satisfied functions.</returns>
        public IEnumerable<SearchedFunctionItem> SearchFunctions(SearchingFunctionFilter filter)
        {
            var criteria = new SearchMenusCriteria
            {
                UserId = filter.UserId,
                Environment = filter.Environment,
                MenuItemName = filter.FunctionName,
                PageSize = filter.PageSize,
                PageIndex = filter.PageIndex
            };
            IEnumerable<SearchedFunctionItem> searchedFunctions = null;
            IEnumerable<SearchedMenuInfo> menus = this.Repository.Fetch<SearchedMenuInfo>(criteria);

            searchedFunctions = menus.Select(obj => new SearchedFunctionItem(obj));
            filter.TotalItemCount = criteria.TotalItemCount;

            return searchedFunctions;
        }
    }
}