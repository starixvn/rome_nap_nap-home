// --------------------------------------------------------------------------
// <copyright file="UpdateMarkedMenuCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.31">Created</change>
// </history>

namespace Fanex.NAP.Home.Sites.Criteria.Common
{
    using Fanex.Data.Repository;

    /// <summary>
    /// The criteria that used to mark/unmark a menu.
    /// </summary>
    public class UpdateMarkedMenuCriteria : CriteriaBase
    {
        public int UserId { get; set; }

        public int SiteId { get; set; }

        public int MenuItemId { get; set; }

        public bool IsMarked { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        /// <summary>
        /// Gets the setting key.
        /// </summary>
        /// <returns>The setting key.</returns>
        public override string GetSettingKey()
        {
            return "NAP_Home_UpdateMarkedMenu";
        }

        /// <summary>
        /// Determines whether this criteria is valid.
        /// </summary>
        /// <returns>Whether this criteria is valid or not.</returns>
        public override bool IsValid()
        {
            return (this.UserId > 0) &&
                   (this.SiteId > 0) &&
                   (this.MenuItemId > 0);
        }
    }
}