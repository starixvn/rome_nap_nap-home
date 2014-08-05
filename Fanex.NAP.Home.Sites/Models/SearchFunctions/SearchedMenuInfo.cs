// --------------------------------------------------------------------------
// <copyright file="SearchedMenuInfo.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.31">Created</change>
// </history>

namespace Fanex.NAP.Home.Sites.Models.SearchFunctions
{
    /// <summary>
    /// Model for searched menu's information.
    /// </summary>
    public class SearchedMenuInfo
    {
        public int? MenuItemId { get; set; }

        public string MenuItemName { get; set; }

        public int? SiteId { get; set; }

        public string SiteName { get; set; }

        public string Link { get; set; }

        public string SiteLink { get; set; }

        public bool? IsMarked { get; set; }

        public string Path { get; set; }
    }
}