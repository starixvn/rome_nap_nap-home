// --------------------------------------------------------------------------
// <copyright file="ISiteBaseService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.29">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.Services
{
    using Fanex.NAP.Framework.Command;
    using Fanex.NAP.Home.Sites.ViewModels.Common;

    public interface ISiteBaseService
    {
        /// <summary>
        /// Updates a function is favorite or un-favorite.
        /// </summary>
        /// <param name="filter">The filter contains function's information.</param>
        /// <returns>Returns if the update process is successful or not.</returns>
        ICommandResult UpdateFavoriteFunction(UpdatingFavoriteFunctionFilter filter);

        /// <summary>
        /// Checks if an user has permission on a function or not.
        /// </summary>
        /// <param name="filter">The filter contains function's information.</param>
        /// <returns>Returns if can access the function or not.</returns>
        ICommandResult CheckUserPermissionOnFunction(ForwardFilter filter);

        /// <summary>
        /// Checks if an user has permission on a site or not.
        /// </summary>
        /// <param name="filter">The filter contains site's information.</param>
        /// <returns>Returns if can access the site or not.</returns>
        ICommandResult CheckUserPermissionOnSite(ForwardFilter filter);
    }
}