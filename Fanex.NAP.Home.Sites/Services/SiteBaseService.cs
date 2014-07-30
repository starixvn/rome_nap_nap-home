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
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Data.Repository;
    using Fanex.NAP.Core.Web.Attribute;
    using Fanex.NAP.Framework.Command;
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
            ICommandResult result = new CommandResult
            {
                Code = 0,
                Message = filter.IsMarked ? "Marked as your favorite" : "Unmarked successfully"
            };

            return result;

            ////using (var db = new ObjectDb(criteria.GetSettingKey()))
            ////{
            ////    var param = criteria.GetSpParams();

            ////    db.ExecuteNonQuery(param);

            ////    return GetCommandResult(param);
            ////}
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
    }
}