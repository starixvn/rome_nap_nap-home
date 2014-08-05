// --------------------------------------------------------------------------
// <copyright file="SiteItem.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.29">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.ViewModels.FavoriteFunctions
{
    using System.Collections.Generic;
    using Fanex.Common;
    using Fanex.NAP.Home.Sites.Models.FavoriteFunctions;

    /// <summary>
    /// View model for site.
    /// </summary>
    public class SiteItem
    {
        public SiteItem()
        {
            this.FavoriteFunctions = new List<FavoriteFunctionItem>();
        }

        public SiteItem(MarkedMenuInfo siteInfo, IEnumerable<FavoriteFunctionItem> favorites)
        {
            if (siteInfo != null)
            {
                this.SiteId = Converter.ToInt(siteInfo.SiteId);
                this.SiteName = siteInfo.SiteName;
                this.SiteLink = siteInfo.SiteLink;
            }

            this.FavoriteFunctions = favorites ?? new List<FavoriteFunctionItem>();
        }

        public int SiteId { get; set; }

        public string SiteName { get; set; }

        public string SiteLink { get; set; }

        public IEnumerable<FavoriteFunctionItem> FavoriteFunctions { get; set; }
    }
}