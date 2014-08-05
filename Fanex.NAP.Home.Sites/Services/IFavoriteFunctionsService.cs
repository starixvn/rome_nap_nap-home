// --------------------------------------------------------------------------
// <copyright file="IFavoriteFunctionsService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.8.4">Created</change>
// </history>

namespace Fanex.NAP.Home.Sites.Services
{
    using System.Collections.Generic;
    using Fanex.NAP.Home.Sites.ViewModels.FavoriteFunctions;

    public interface IFavoriteFunctionsService : ISiteBaseService
    {
        /// <summary>
        /// Gets the marked functions.
        /// </summary>
        /// <param name="filter">The object contains filter conditions.</param>
        /// <returns>Favorite functions grouped by site IDs and site names.</returns>
        IEnumerable<SiteItem> LoadMarkedFunctions(GettingFavoriteFunctionsFilter filter);
    }
}