// --------------------------------------------------------------------------
// <copyright file="SitesInfo.cs">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.9.1">Created</change>
// </history>

namespace Fanex.NAP.Home.Sites.Models.UserSites
{
    using System.Collections.Generic;

    /// <summary>
    /// Contains list of sites URLs and their menus.
    /// </summary>
    public class SitesInfo
    {
        public IEnumerable<SiteUrlInfo> SiteUrlList { get; set; }

        public IEnumerable<MenuInfo> MenuList { get; set; }
    }
}