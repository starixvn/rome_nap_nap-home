// --------------------------------------------------------------------------
// <copyright file="SearchFunctionsFilter.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.9.1">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.ViewModels.UserSites
{
    /// <summary>
    /// The filter object that is used to query sites and functions by function name.
    /// </summary>
    public class SearchFunctionsFilter
    {
        public string SearchedFunctionName { get; set; }

        public int UserId { get; set; }

        public string Environment { get; set; }
    }
}