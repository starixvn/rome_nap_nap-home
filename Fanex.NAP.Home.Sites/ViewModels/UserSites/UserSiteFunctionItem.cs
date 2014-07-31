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
    using Fanex.NAP.Home.Sites.Models.UserSites;

    /// <summary>
    /// View model for a function of a site.
    /// </summary>
    public class UserSiteFunctionItem
    {
        public UserSiteFunctionItem()
        {
            this.Children = new List<UserSiteFunctionItem>();
        }

        public UserSiteFunctionItem(MenuInfo menu)
        {
            if (menu != null)
            {
                this.FunctionId = menu.MenuItemId;
                this.FunctionName = menu.MenuItemName;
                this.SiteId = menu.SiteId;
                this.Link = menu.Link;
                this.IsFavorite = menu.IsMarked;
                this.Children = new List<UserSiteFunctionItem>();
            }
        }

        public UserSiteFunctionItem(TopMarkedMenuInfo menu)
        {
            if (menu != null)
            {
                this.FunctionId = menu.MenuItemId;
                this.FunctionName = menu.MenuItemName;
                this.SiteId = menu.SiteId;
                this.Link = menu.Link;
                this.IsFavorite = true;
            }
        }

        public int FunctionId { get; set; }

        public string FunctionName { get; set; }

        public int SiteId { get; set; }

        public string Link { get; set; }

        public bool IsFavorite { get; set; }

        public List<UserSiteFunctionItem> Children { get; set; }
    }
}