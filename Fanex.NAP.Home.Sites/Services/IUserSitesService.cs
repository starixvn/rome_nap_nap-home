// --------------------------------------------------------------------------
// <copyright file="IUserSitesService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.29">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.Services
{
    using System.Collections.Generic;
    using Fanex.NAP.Home.Sites.ViewModels.UserSites;

    public interface IUserSitesService
    {
        /// <summary>
        /// Gets accessible sites of an user.
        /// </summary>
        /// <returns>List of sites that the user can access.</returns>
        IEnumerable<UserSiteItem> QuerytUserSites();
    }
}