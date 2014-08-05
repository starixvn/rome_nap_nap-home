// --------------------------------------------------------------------------
// <copyright file="GettingFavoriteFunctionsFilter.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.8.4">Created</change>
// </history>

namespace Fanex.NAP.Home.Sites.ViewModels.FavoriteFunctions
{
    /// <summary>
    /// Getting marked functions filter.
    /// </summary>
    public class GettingFavoriteFunctionsFilter
    {
        public GettingFavoriteFunctionsFilter()
        {
            this.PageIndex = 1;
        }

        public int UserId { get; set; }

        public string Environment { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public int TotalItemCount { get; set; }
    }
}