// --------------------------------------------------------------------------
// <copyright file="GetSiteMenusByUserIdCriteria.cs">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.9.1">Created</change>
// </history>

using Fanex.Data.Repository;

namespace Fanex.NAP.Home.Sites.Criteria.UserSites
{
    /// <summary>
    /// Get list of sites and menus that an user can access.
    /// </summary>
    public class GetSiteMenusByUserIdCriteria : CriteriaBase
    {
        public int UserId { get; set; }

        public string Environment { get; set; }

        public string MenuItemName { get; set; }

        /// <summary>
        /// Gets the setting key.
        /// </summary>
        /// <returns>The setting key.</returns>
        public override string GetSettingKey()
        {
            return "NAP_Home_MainPage";
        }

        /// <summary>
        /// Determines whether this criteria is valid.
        /// </summary>
        /// <returns>Whether this criteria is valid or not.</returns>
        public override bool IsValid()
        {
            return (this.UserId > 0);
        }
    }
}