// --------------------------------------------------------------------------
// <copyright file="UserSitesRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.31">Created</change>
//   <change who="Jason.Ly" date="2014.9.1">Add CheckUserPermissionOnFunction and CheckUserPermissionOnSite</change>
// </history>

namespace Fanex.NAP.Home.Sites.Repositories
{
    using System;
    using System.Collections;
    using Fanex.Common;
    using Fanex.Data;
    using Fanex.NAP.Framework.Command;
    using Fanex.NAP.Home.Sites.Criteria.UserSites;
    using Fanex.NAP.Home.Sites.Models.UserSites;

    /// <summary>
    /// User sites repository.
    /// </summary>
    public class UserSitesRepository : IUserSitesRepository
    {
        /// <summary>
        /// Gets all sites and their menus of an user.
        /// </summary>
        /// <param name="criteria">The criteria that are use to query data.</param>
        /// <returns>SitesInfo object contains a list of site URLs and a list of sites' menus.</returns>
        public SitesInfo GetSitesAndMenus(GetSiteMenusByUserIdCriteria criteria)
        {
            SitesInfo siteData = new SitesInfo();
            Hashtable param = criteria.GetSpParams();

            using (IObjectDb db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (GridReader mult = db.QueryMultiple(param))
                {
                    siteData.SiteUrlList = mult.Read<SiteUrlInfo>();
                    siteData.MenuList = mult.Read<MenuInfo>();
                }
            }

            return siteData;
        }

        /// <summary>
        /// Checks if an user has permission on a function or not.
        /// </summary>
        /// <param name="criteria">The criteria contain function's information.</param>
        /// <returns>Returns if can access the function or not.</returns>
        public ICommandResult CheckUserPermissionOnFunction(CheckPermissionByMenuUserIdCriteria criteria)
        {
            CommandResult result = null;

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                Hashtable param = criteria.GetSpParams();

                db.ExecuteNonQuery(param);

                result = GetCommandResult(param);
            }

            return result;
        }

        /// <summary>
        /// Checks if an user has permission on a site or not.
        /// </summary>
        /// <param name="criteria">The criteria contain site's information.</param>
        /// <returns>Returns if can access the site or not.</returns>
        public ICommandResult CheckUserPermissionOnSite(CheckUserPermissionBySiteCriteria criteria)
        {
            CommandResult result = null;

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                Hashtable param = criteria.GetSpParams();

                db.ExecuteNonQuery(param);

                result = GetCommandResult(param);
            }

            return result;
        }

        /// <summary>
        /// Gets the command result.
        /// </summary>
        /// <param name="hash">The hash table.</param>
        /// <returns>CommandResult object contains error code and error message.</returns>
        private CommandResult GetCommandResult(Hashtable hash)
        {
            var errCode = hash["errcode"];
            var errMsg = hash["errmsg"];

            return new CommandResult
            {
                Code = (errCode is DBNull) ? 0 : Converter.ToInt(errCode),
                Message = (errMsg is DBNull) ? string.Empty : Converter.ToString2(errMsg)
            };
        }
    }
}