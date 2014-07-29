// --------------------------------------------------------------------------
// <copyright file="UpdatingFavoriteFunctionFilter.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.29">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.ViewModels.UserSites
{
    /// <summary>
    /// Mark or unmark a function is favorite.
    /// </summary>
    public class UpdatingFavoriteFunctionFilter
    {
        public int SiteId { get; set; }

        public int UserId { get; set; }

        public int FunctionId { get; set; }
    }
}