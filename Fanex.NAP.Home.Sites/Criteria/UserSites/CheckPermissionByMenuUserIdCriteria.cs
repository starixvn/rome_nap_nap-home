// --------------------------------------------------------------------------
// <copyright file="CheckPermissionByMenuUserIdCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.8.8">Created</change>
// </history>

namespace Fanex.NAP.Home.Sites.Criteria.UserSites
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// The criteria that are used to check if an user has permission on a specific menu or not.
    /// </summary>
    public class CheckPermissionByMenuUserIdCriteria : CriteriaBase
    {
        public int UserId { get; set; }

        public int SiteId { get; set; }

        public int MenuId { get; set; }

        public string Environment { get; set; }

        public string ErrMsg { get; set; }

        public int ErrCode { get; set; }

        /// <summary>
        /// Gets the setting key.
        /// </summary>
        /// <returns>The setting key.</returns>
        public override string GetSettingKey()
        {
            return "NAP_Home_CheckPermissionByMenuUserId";
        }

        /// <summary>
        /// Determines whether this criteria is valid.
        /// </summary>
        /// <returns>Whether this criteria is valid or not.</returns>
        public override bool IsValid()
        {
            return (this.UserId > 0) &&
                   (this.SiteId > 0) &&
                   (this.MenuId > 0) &&
                   !string.IsNullOrEmpty(this.Environment);
        }
    }
}