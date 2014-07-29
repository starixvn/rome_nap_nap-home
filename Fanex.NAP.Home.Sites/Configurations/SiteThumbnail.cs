// --------------------------------------------------------------------------
// <copyright file="SiteThumbnail.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.29">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.Configurations
{
    /// <summary>
    /// Entity contains configuration data for site's thumbnail using in Home page.
    /// </summary>
    public class SiteThumbnail
    {
        public int SiteId { get; set; }

        public string Name { get; set; }

        public string ImageName { get; set; }
    }
}