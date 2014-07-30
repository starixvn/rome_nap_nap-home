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
    /// <summary>
    /// View model demonstrates an searched function in Search Results page.
    /// </summary>
    public class SearchedFunctionItem
    {
        public int SiteId { get; set; }

        public int FunctionId { get; set; }

        public string FunctionName { get; set; }

        public string Link { get; set; }

        public string Path { get; set; }

        public bool IsFavorite { get; set; }
    }
}