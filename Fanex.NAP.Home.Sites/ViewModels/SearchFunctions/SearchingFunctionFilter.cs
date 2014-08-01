// --------------------------------------------------------------------------
// <copyright file="SearchingFunctionFilter.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.29">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.ViewModels.SearchFunctions
{
    /// <summary>
    /// Filter is used for searching functions.
    /// </summary>
    public class SearchingFunctionFilter
    {
        public SearchingFunctionFilter()
        {
            this.PageSize = 10;
        }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public int TotalItemCount { get; set; }

        public string FunctionName { get; set; }

        public int UserId { get; set; }

        public string Environment { get; set; }
    }
}