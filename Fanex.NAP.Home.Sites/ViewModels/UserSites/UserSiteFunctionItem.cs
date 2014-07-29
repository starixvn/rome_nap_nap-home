// --------------------------------------------------------------------------
// <copyright file="UserSiteFunctionItem.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.29">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.ViewModels.UserSites
{
    using System.Collections.Generic;

    /// <summary>
    /// View model for a function of a site.
    /// </summary>
    public class UserSiteFunctionItem
    {
        public UserSiteFunctionItem()
        {
            this.Children = new List<UserSiteFunctionItem>();
        }

        public int FunctionId { get; set; }

        public string FunctionName { get; set; }

        public string Url { get; set; }

        public bool IsFavorite { get; set; }

        public List<UserSiteFunctionItem> Children { get; set; }
    }
}