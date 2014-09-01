// --------------------------------------------------------------------------
// <copyright file="FunctionItem.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.9.1">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.ViewModels.UserSites
{
    using System;
    using Fanex.NAP.Home.Core.Helpers;
    using Fanex.NAP.Home.Sites.Models.UserSites;

    /// <summary>
    /// Demonstrate function information.
    /// </summary>
    [Serializable]
    public class FunctionItem
    {
        public FunctionItem()
        {
        }

        public FunctionItem(MenuInfo menu)
        {
            if (menu != null && menu.MenuItemId.HasValue)
            {
                this.FunctionId = menu.MenuItemId.Value;
                this.FunctionName = menu.MenuItemName;
                this.FunctionLink = StringExtensions.TrimOrEmpty(menu.MenuLink);
            }
        }

        public FunctionItem(FunctionItem item)
        {
            if (item != null)
            {
                this.FunctionId = item.FunctionId;
                this.FunctionName = item.FunctionName;
                this.FunctionLink = item.FunctionLink;
            }
        }

        public int FunctionId { get; set; }

        public string FunctionName { get; set; }

        public string FunctionLink { get; set; }

        public int DisplayOrder { get; set; }
    }
}