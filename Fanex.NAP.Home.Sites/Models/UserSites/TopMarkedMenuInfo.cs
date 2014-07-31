// --------------------------------------------------------------------------
// <copyright file="TopMarkedMenuInfo.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.31">Created</change>
// </history>

namespace Fanex.NAP.Home.Sites.Models.UserSites
{
    /// <summary>
    /// Containing marked menu's information belongs to the top.
    /// </summary>
    public class TopMarkedMenuInfo
    {
        public byte SiteId { get; set; }

        public int MenuItemId { get; set; }

        public string MenuItemName { get; set; }

        public string Link { get; set; }
    }
}