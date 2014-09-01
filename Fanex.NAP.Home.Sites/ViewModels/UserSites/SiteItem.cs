// --------------------------------------------------------------------------
// <copyright file="SiteItem.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.9.1">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.ViewModels.UserSites
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.NAP.Home.Core.Helpers;
    using Fanex.NAP.Home.Sites.Configurations;
    using Fanex.NAP.Home.Sites.Models.UserSites;

    /// <summary>
    /// View model contains information of a site.
    /// </summary>
    [Serializable]
    public class SiteItem
    {
        public SiteItem()
        {
        }

        public SiteItem(SiteUrlInfo site, List<string> siteLinks, List<FunctionGroup> functionGroups)
        {
            if (site != null && site.SiteId.HasValue)
            {
                this.SiteId = site.SiteId.Value;
                this.SiteName = site.SiteName;
                this.Thumbnail = SiteThumbnailsManager.GetImageName(this.SiteId);
            }

            if (siteLinks != null && siteLinks.Count > 0)
            {
                this.DefaultLink = StringExtensions.TrimOrEmpty(siteLinks[0]);
                siteLinks.RemoveAt(0);
                this.OtherLinkList = siteLinks.Select(link => StringExtensions.TrimOrEmpty(link)).ToList();
            }

            this.FunctionGroupList = functionGroups ?? new List<FunctionGroup>();
        }

        public int SiteId { get; set; }

        public string SiteName { get; set; }

        public string DefaultLink { get; set; }

        public string Thumbnail { get; set; }

        public List<string> OtherLinkList { get; set; }

        public List<FunctionGroup> FunctionGroupList { get; set; }
    }
}