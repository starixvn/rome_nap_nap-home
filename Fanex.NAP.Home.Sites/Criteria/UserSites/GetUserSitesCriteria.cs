// --------------------------------------------------------------------------
// <copyright file="GetUserSitesCriteria.cs" company="Nexcel Solutions Vietnam">
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
    /// The criteria that are used to get all accessible sites of an user.
    /// </summary>
    public class GetUserSitesCriteria : CriteriaBase
    {
        public int UserId { get; set; }

        public string Environment { get; set; }

        /// <summary>
        /// Gets the setting key.
        /// </summary>
        /// <returns>The setting key.</returns>
        public override string GetSettingKey()
        {
            return "NAP_Home_GetUserSites";
        }

        /// <summary>
        /// Determines whether this criteria is valid.
        /// </summary>
        /// <returns>Whether this criteria is valid or not.</returns>
        public override bool IsValid()
        {
            return (this.UserId > 0) && !string.IsNullOrEmpty(this.Environment);
        }
    }
}