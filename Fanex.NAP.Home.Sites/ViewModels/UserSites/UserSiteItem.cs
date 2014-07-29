// --------------------------------------------------------------------------
// <copyright file="UserSiteItem.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.29">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.ViewModels.UserSites
{
    using System.Collections.Generic;

    /// <summary>
    /// View model for accessible site of an user.
    /// </summary>
    public class UserSiteItem
    {
        public UserSiteItem()
        {
            this.Domains = new List<string>();
            this.Favorites = new List<UserSiteFunctionItem>();
        }

        public int SiteId { get; set; }

        public string SiteName { get; set; }

        public string Thumbnail { get; set; }

        public List<string> Domains { get; set; }

        public List<UserSiteFunctionItem> Favorites { get; set; }
    }
}