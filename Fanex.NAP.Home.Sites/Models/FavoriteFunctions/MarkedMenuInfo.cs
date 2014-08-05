// --------------------------------------------------------------------------
// <copyright file="MarkedMenuInfo.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.8.4">Created</change>
// </history>

namespace Fanex.NAP.Home.Sites.Models.FavoriteFunctions
{
    /// <summary>
    /// Model for marked menu.
    /// </summary>
    public class MarkedMenuInfo
    {
        public int? MenuItemId { get; set; }

        public int? SiteId { get; set; }

        public string SiteName { get; set; }

        public string SiteLink { get; set; }

        public string Link { get; set; }

        public string Path { get; set; }
    }
}