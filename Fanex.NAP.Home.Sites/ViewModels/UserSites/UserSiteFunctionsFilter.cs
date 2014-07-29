// --------------------------------------------------------------------------
// <copyright file="UserSiteFunctionsFilter.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.29">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.ViewModels.UserSites
{
    /// <summary>
    /// Query available functions belong to a site of user.
    /// </summary>
    public class UserSiteFunctionsFilter
    {
        public int UserId { get; set; }

        public int SiteId { get; set; }

        public string Environment { get; set; }
    }
}