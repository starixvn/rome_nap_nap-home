// --------------------------------------------------------------------------
// <copyright file="UserSitesFilter.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.29">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.ViewModels.UserSites
{
    /// <summary>
    /// Query available sites of user.
    /// </summary>
    public class UserSitesFilter
    {
        public int UserId { get; set; }

        public string Environment { get; set; }
    }
}