// --------------------------------------------------------------------------
// <copyright file="GetMarkedMenusCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.8.4">Created</change>
// </history>

namespace Fanex.NAP.Home.Sites.Criteria.FavoriteFunctions
{
    using Fanex.Data.Repository;

    /// <summary>
    /// The criteria that are used to get all marked menus.
    /// </summary>
    public class GetMarkedMenusCriteria : CriteriaBase
    {
        public int UserId { get; set; }

        public string Environment { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public int TotalItemCount { get; set; }

        /// <summary>
        /// Gets the setting key.
        /// </summary>
        /// <returns>The setting key.</returns>
        public override string GetSettingKey()
        {
            return "NAP_Home_GetMarkedMenus";
        }

        /// <summary>
        /// Determines whether this criteria is valid.
        /// </summary>
        /// <returns>Whether this criteria is valid or not.</returns>
        public override bool IsValid()
        {
            return (this.UserId > 0) &&
                   !string.IsNullOrEmpty(this.Environment);
        }
    }
}