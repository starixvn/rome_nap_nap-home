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
    using System.Linq;
    using Fanex.NAP.Home.Core.Helpers;
    using Fanex.NAP.Home.Sites.Configurations;
    using Fanex.NAP.Home.Sites.Models.UserSites;

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

        public UserSiteItem(SiteInfo site, List<string> domains, List<UserSiteFunctionItem> favorites)
        {
            if (site != null)
            {
                this.SiteId = site.SiteId;
                this.SiteName = site.SiteName;
                this.Thumbnail = SiteThumbnailsManager.GetImageName(site.SiteId);
                this.Domains = domains ?? new List<string>();
                this.Favorites = favorites ?? new List<UserSiteFunctionItem>();

                if (this.Domains.Count > 0)
                {
                    this.DefaultLink = StringExtensions.TrimOrEmpty(this.Domains[0]);
                    this.Domains.RemoveAt(0);
                    this.Domains = this.Domains.Select(domain => StringExtensions.TrimOrEmpty(domain)).ToList();
                }
            }
        }

        public int SiteId { get; set; }

        public string SiteName { get; set; }

        public string DefaultLink { get; set; }

        public string Thumbnail { get; set; }

        public List<string> Domains { get; set; }

        public List<UserSiteFunctionItem> Favorites { get; set; }
    }
}