// --------------------------------------------------------------------------
// <copyright file="SearchMenusCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.31">Created</change>
// </history>

namespace Fanex.NAP.Home.Sites.Criteria.SearchFunctions
{
    using Fanex.Data.Repository;

    /// <summary>
    /// The criteria that are used to search menus.
    /// </summary>
    public class SearchMenusCriteria : CriteriaBase
    {
        public int UserId { get; set; }

        public string MenuItemName { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public int TotalItemCount { get; set; }

        /// <summary>
        /// Gets the setting key.
        /// </summary>
        /// <returns>The setting key.</returns>
        public override string GetSettingKey()
        {
            return "NAP_Home_SearchMenus";
        }

        /// <summary>
        /// Determines whether this criteria is valid.
        /// </summary>
        /// <returns>Whether this criteria is valid or not.</returns>
        public override bool IsValid()
        {
            return (this.UserId > 0) &&
                   (this.PageSize > 0) &&
                   !string.IsNullOrEmpty(this.MenuItemName);
        }
    }
}