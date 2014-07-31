// --------------------------------------------------------------------------
// <copyright file="MenuInfo.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.31">Created</change>
// </history>

namespace Fanex.NAP.Home.Sites.Models.UserSites
{
    /// <summary>
    /// Model contains all information of a menu.
    /// </summary>
    public class MenuInfo
    {
        public byte SiteId { get; set; }

        public int MenuItemId { get; set; }

        public string MenuItemName { get; set; }

        public short? ParentId { get; set; }

        public string ParentName { get; set; }

        public string Link { get; set; }

        public short DisplayOrder { get; set; }

        public bool IsMarked { get; set; }
    }
}