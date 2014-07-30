// --------------------------------------------------------------------------
// <copyright file="ISearchFunctionsService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.29">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.Services
{
    using System.Collections.Generic;
    using Fanex.NAP.Home.Sites.ViewModels.SearchFunctions;

    public interface ISearchFunctionsService : ISiteBaseService
    {
        /// <summary>
        /// Gets all functions which satisfy search conditions.
        /// </summary>
        /// <param name="filter">The filter object contains searching conditions.</param>
        /// <returns>List of satisfied functions.</returns>
        IEnumerable<SearchedFunctionItem> SearchFunctions(SearchingFunctionFilter filter);
    }
}