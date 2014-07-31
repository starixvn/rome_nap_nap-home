// --------------------------------------------------------------------------
// <copyright file="GetTopMarkedMenusCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.31">Created</change>
// </history>

namespace Fanex.NAP.Home.Sites.Criteria.UserSites
{
    using Fanex.Data.Repository;

    /// <summary>
    /// The criteria that are used to get top marked menus to display to the site's tile.
    /// </summary>
    public class GetTopMarkedMenusCriteria : CriteriaBase
    {
        public int UserId { get; set; }

        public string SiteIds { get; set; }

        public string Environment { get; set; }

        public int TopSelect { get; set; }

        /// <summary>
        /// Gets the setting key.
        /// </summary>
        /// <returns>The setting key.</returns>
        public override string GetSettingKey()
        {
            return "NAP_Home_GetTopMarkedMenus";
        }

        /// <summary>
        /// Determines whether this criteria is valid.
        /// </summary>
        /// <returns>Whether this criteria is valid or not.</returns>
        public override bool IsValid()
        {
            return (this.UserId > 0) &&
                   (this.TopSelect > 0) &&
                   !string.IsNullOrEmpty(this.SiteIds) &&
                   !string.IsNullOrEmpty(this.Environment);
        }
    }
}