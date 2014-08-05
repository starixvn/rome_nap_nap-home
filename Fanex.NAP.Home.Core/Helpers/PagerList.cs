// --------------------------------------------------------------------------
// <copyright file="PagerList.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.28">Create</change>
// </history>

namespace Fanex.NAP.Home.Core.Helpers
{
    using System.Collections.Generic;
    using Webdiyer.WebControls.Mvc;

    /// <summary>
    /// Page list helper.
    /// </summary>
    /// <typeparam name="T">Object type.</typeparam>
    public class PagerList<T> : List<T>, IPagedList
    {
        public PagerList(int pageIndex, int pageSize, int totalItemCount, IEnumerable<T> items)
        {
            this.CurrentPageIndex = pageIndex;
            this.PageSize = pageSize;
            this.TotalItemCount = totalItemCount;
            this.AddRange(items);
        }

        public int CurrentPageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalItemCount { get; set; }

        public bool HasPagers
        {
            get
            {
                return this.TotalItemCount > this.PageSize;
            }
        }
    }
}