// --------------------------------------------------------------------------
// <copyright file="FavoriteFunctionItem.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.29">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.ViewModels.FavoriteFunctions
{
    using Fanex.Common;
    using Fanex.NAP.Home.Sites.Models.FavoriteFunctions;

    /// <summary>
    /// Marked function view model.
    /// </summary>
    public class FavoriteFunctionItem
    {
        public FavoriteFunctionItem()
        {
        }

        public FavoriteFunctionItem(MarkedMenuInfo menu)
        {
            if (menu != null)
            {
                this.FunctionId = Converter.ToInt(menu.MenuItemId);
                this.Link = menu.Link;
                this.Path = menu.Path;
            }
        }

        public int FunctionId { get; set; }

        public string Link { get; set; }

        public string Path { get; set; }
    }
}