// --------------------------------------------------------------------------
// <copyright file="SiteUrlInfo.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.9.1">Created</change>
// </history>

namespace Fanex.NAP.Home.Sites.Models.UserSites
{
    /// <summary>
    /// Model for basic site URLs' information.
    /// </summary>
    public class SiteUrlInfo
    {
        public int? SiteId { get; set; }

        public string SiteName { get; set; }

        public string SiteLink { get; set; }

        public bool IsLocalLink
        {
            get
            {
                return SiteId.HasValue && SiteId == 83 &&
                    SiteLink == "http://10.18.200.202:8015/napcrm";
            }
        }

        public bool? IsDefault { get; set; }
    }
}