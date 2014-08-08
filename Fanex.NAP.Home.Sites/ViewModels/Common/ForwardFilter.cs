// --------------------------------------------------------------------------
// <copyright file="ForwardFilter.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.8.8">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.ViewModels.Common
{
    /// <summary>
    /// Forward to s specific site or function.
    /// </summary>
    public class ForwardFilter
    {
        public int UserId { get; set; }

        public string Environment { get; set; }

        public int SiteId { get; set; }

        public string SiteLink { get; set; }

        public int? FunctionId { get; set; }

        public string FunctionLink { get; set; }
    }
}