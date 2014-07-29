// --------------------------------------------------------------------------
// <copyright file="FavoriteFunctionsFilter.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.29">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.ViewModels.UserSites
{
    /// <summary>
    /// Query top favorite functions belong to a site of an user..
    /// </summary>
    public class FavoriteFunctionsFilter
    {
        public int SiteId { get; set; }

        public int UserId { get; set; }

        public string Environment { get; set; }
    }
}