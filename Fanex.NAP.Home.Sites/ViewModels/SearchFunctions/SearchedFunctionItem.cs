// --------------------------------------------------------------------------
// <copyright file="SearchedFunctionItem.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.29">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.ViewModels.SearchFunctions
{
    using Fanex.Common;
    using Fanex.NAP.Home.Core.Helpers;
    using Fanex.NAP.Home.Sites.Models.SearchFunctions;

    /// <summary>
    /// View model demonstrates an searched function in Search Results page.
    /// </summary>
    public class SearchedFunctionItem
    {
        public SearchedFunctionItem()
        {
        }

        public SearchedFunctionItem(SearchedMenuInfo menuInfo)
        {
            if (menuInfo != null)
            {
                this.SiteId = Converter.ToInt(menuInfo.SiteId);
                this.FunctionId = Converter.ToInt(menuInfo.MenuItemId);
                this.FunctionName = menuInfo.MenuItemName;
                this.Link = StringExtensions.TrimOrEmpty(menuInfo.Link);
                this.SiteLink = StringExtensions.TrimOrEmpty(menuInfo.SiteLink);
                this.SiteName = menuInfo.SiteName;
                this.Path = menuInfo.Path;
                this.IsFavorite = Converter.ToBool(menuInfo.IsMarked);
            }
        }

        public int SiteId { get; set; }

        public string SiteName { get; set; }

        public string SiteLink { get; set; }

        public int FunctionId { get; set; }

        public string FunctionName { get; set; }

        public string Link { get; set; }

        public string Path { get; set; }

        public bool IsFavorite { get; set; }
    }
}