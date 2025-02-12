// --------------------------------------------------------------------------
// <copyright file="CheckUserPermissionBySiteCriteria.cs">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.8.8">Created</change>
// </history>

using Fanex.Data.Repository;

namespace Fanex.NAP.Home.Sites.Criteria.UserSites
{
    /// <summary>
    /// Check user's permission on site.
    /// </summary>
    public class CheckUserPermissionBySiteCriteria : NonQueryCommand
    {
        public int UserId { get; set; }

        public int SiteId { get; set; }

        public string Environment { get; set; }

        public string ErrMsg { get; set; }

        public int ErrCode { get; set; }

        /// <summary>
        /// Gets the setting key.
        /// </summary>
        /// <returns>The setting key.</returns>
        public override string GetSettingKey()
        {
            return "NAP_Home_CheckUserPermissionBySite";
        }

        /// <summary>
        /// Determines whether this criteria is valid.
        /// </summary>
        /// <returns>Whether this criteria is valid or not.</returns>
        public override bool IsValid()
        {
            return (this.UserId > 0) &&
                   (this.SiteId > 0) &&
                   !string.IsNullOrEmpty(this.Environment);
        }
    }
}