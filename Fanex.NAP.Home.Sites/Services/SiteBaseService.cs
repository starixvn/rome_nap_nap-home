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
    using System.Diagnostics.CodeAnalysis;
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
        /// Temperate function to skip FXCop warning.
        /// </summary>
        /// <param name="filter">Filter condition.</param>
        /// <returns>Returns fake object.</returns>
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Temperately using.")]
        protected object QueryData(object filter)
        {
            return null;
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