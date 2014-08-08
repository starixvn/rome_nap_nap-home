// --------------------------------------------------------------------------
// <copyright file="SiteBaseService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.29">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.Services
{
    using System;
    using System.Collections;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using Fanex.NAP.Core.Web.Attribute;
    using Fanex.NAP.Framework.Command;
    using Fanex.NAP.Home.Core.Resources;
    using Fanex.NAP.Home.Sites.Criteria.Common;
    using Fanex.NAP.Home.Sites.ViewModels.Common;

    /// <summary>
    /// Base service.
    /// </summary>
    [NapSuperAuth]
    public class SiteBaseService : ISiteBaseService
    {
        public SiteBaseService()
        {
            this.Repository = new DynamicRepository();
        }

        protected IDynamicRepository Repository { get; set; }

        /// <summary>
        /// Updates a function is favorite or un-favorite.
        /// </summary>
        /// <param name="filter">The filter contains function's information.</param>
        /// <returns>Returns if the update process is successful or not.</returns>
        public ICommandResult UpdateFavoriteFunction(UpdatingFavoriteFunctionFilter filter)
        {
            var criteria = new UpdateMarkedMenuCriteria
            {
                SiteId = filter.SiteId,
                MenuItemId = filter.FunctionId,
                IsMarked = filter.IsMarked,
                UserId = filter.UserId
            };
            CommandResult result = null;

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                Hashtable param = criteria.GetSpParams();

                db.ExecuteNonQuery(param);

                result = GetCommandResult(param);
            }

            if (result != null && result.Code == 0)
            {
                result.Message = filter.IsMarked ? ViewLang.MarkedAsYourFavorite : ViewLang.UnmarkedSuccessfully;
            }

            return result;
        }

        /// <summary>
        /// Checks if an user has permission on a function or not.
        /// </summary>
        /// <param name="filter">The filter contains function's information.</param>
        /// <returns>Returns if can access the function or not.</returns>
        public ICommandResult CheckUserPermissionOnFunction(ForwardFilter filter)
        {
            var criteria = new CheckPermissionByMenuUserIdCriteria
            {
                SiteId = filter.SiteId,
                MenuId = filter.FunctionId.Value,
                UserId = filter.UserId,
                Environment = filter.Environment
            };
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
        /// <param name="filter">The filter contains site's information.</param>
        /// <returns>Returns if can access the site or not.</returns>
        public ICommandResult CheckUserPermissionOnSite(ForwardFilter filter)
        {
            var criteria = new CheckUserPermissionBySiteCriteria
            {
                SiteId = filter.SiteId,
                UserId = filter.UserId,
                Environment = filter.Environment
            };
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
                Code = (errCode is DBNull) ? 0 : Convert.ToInt32(errCode),
                Message = (errMsg is DBNull) ? string.Empty : Convert.ToString(errMsg)
            };
        }
    }
}