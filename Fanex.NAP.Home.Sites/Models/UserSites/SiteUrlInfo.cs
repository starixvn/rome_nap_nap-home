// --------------------------------------------------------------------------
// <copyright file="SiteUrlInfo.cs">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.9.1">Created</change>
// </history>

namespace Fanex.NAP.Home.Sites.Models.UserSites
{
    using Fanex.NAP.Home.Core.Helpers;

    /// <summary>
    /// Model for basic site URLs' information.
    /// </summary>
    public class SiteUrlInfo
    {
        public int? SiteId { get; set; }

        public string SiteName { get; set; }

        public string SiteLink { get; set; }

        public string Environment { get; set; }

        public bool IsLocalLink
        {
            get
            {
                return IPHelper.IsLocalLink(SiteLink);
            }
        }

        public bool? IsDefault { get; set; }
    }
}