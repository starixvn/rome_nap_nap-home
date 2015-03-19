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
    using Fanex.NAP.Home.Sites.Criteria.UserSites;

    /// <summary>
    /// View model contains information of a site.
    /// </summary>
    [Serializable]
    public class SiteItem
    {
        public SiteItem()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteItem"/> class.
        /// </summary>
        /// <param name="createSiteItemCriteria">The create site item criteria.</param>
        public SiteItem(CreateSiteItemCriteria createSiteItemCriteria)
        {
            if (createSiteItemCriteria.SiteInfo != null && createSiteItemCriteria.SiteInfo.SiteId.HasValue)
            {
                this.SiteId = createSiteItemCriteria.SiteInfo.SiteId.Value;
                this.SiteName = createSiteItemCriteria.SiteInfo.SiteName;
                this.Thumbnail = SiteThumbnailsManager.GetImageName(this.SiteId);
            }

            this.FunctionGroupList = createSiteItemCriteria.FunctionGroups ?? new List<FunctionGroup>();
            if (createSiteItemCriteria.SiteLinks == null ||
                !createSiteItemCriteria.SiteLinks.Any())
            {
                return;
            }

            var firstLink = createSiteItemCriteria.SiteLinks.Where(link => link.IsLocalLink).FirstOrDefault();
            if (createSiteItemCriteria.IsWebVpnAccess && firstLink != null && firstLink.IsLocalLink)
            {
                this.DefaultLink = StringExtensions.TrimOrEmpty(firstLink.SiteLink);
                this.OtherLinkList = null;
                return;
            }

            var nonLocalLinks = createSiteItemCriteria.SiteLinks.Where(siteLink => !siteLink.IsLocalLink).ToList();
            if (nonLocalLinks == null || !nonLocalLinks.Any())
            {
                this.DefaultLink = StringExtensions.TrimOrEmpty(firstLink.SiteLink);
                this.OtherLinkList = null;
                return;
            }

            this.DefaultLink = StringExtensions.TrimOrEmpty(nonLocalLinks[0].SiteLink);
            nonLocalLinks.RemoveAt(0);
            this.OtherLinkList = nonLocalLinks.Select(link => StringExtensions.TrimOrEmpty(link.SiteLink))
                .ToList();
        }

        public int SiteId { get; set; }

        public string SiteName { get; set; }

        public string DefaultLink { get; set; }

        public string Thumbnail { get; set; }

        public List<string> OtherLinkList { get; set; }

        public List<FunctionGroup> FunctionGroupList { get; set; }
    }
}