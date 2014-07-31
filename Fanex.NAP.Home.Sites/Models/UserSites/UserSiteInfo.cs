// --------------------------------------------------------------------------
// <copyright file="UserSiteInfo.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.31">Created</change>
// </history>

namespace Fanex.NAP.Home.Sites.Models.UserSites
{
    using System.Collections.Generic;

    /// <summary>
    /// Wrapper class for user site's information.
    /// </summary>
    public class UserSiteInfo
    {
        public IEnumerable<SiteInfo> Sites { get; set; }

        public IEnumerable<SiteUrlInfo> SiteUrls { get; set; }

        public IEnumerable<TopMarkedMenuInfo> TopMarkedMenus { get; set; }
    }
}