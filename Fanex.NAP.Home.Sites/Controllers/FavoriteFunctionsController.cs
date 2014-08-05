// --------------------------------------------------------------------------
// <copyright file="FavoriteFunctionsController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.29">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Fanex.NAP.Core.Web.Attribute;
    using Fanex.NAP.Home.Core.Helpers;
    using Fanex.NAP.Home.Sites.Services;
    using Fanex.NAP.Home.Sites.ViewModels.FavoriteFunctions;

    /// <summary>
    /// Favorite functions controller.
    /// </summary>
    [NapSuperAuth]
    public class FavoriteFunctionsController : SiteBaseController
    {
        private IFavoriteFunctionsService _service;

        public FavoriteFunctionsController() :
            this(null)
        {
        }

        public FavoriteFunctionsController(IFavoriteFunctionsService service)
        {
            this._service = service ?? new FavoriteFunctionsService();
        }

        /// <summary>
        /// Redirect to Favorite Functions page.
        /// </summary>
        /// <param name="filter">Filter conditions.</param>
        /// <returns>Return Favorite Functions page.</returns>
        [HttpGet]
        public ActionResult Index(GettingFavoriteFunctionsFilter filter)
        {
            IEnumerable<SiteItem> favorites = null;
            PagerList<SiteItem> pagedFavorites = null;

            filter.PageSize = AppSettings.SearchPageSize;
            filter.UserId = this.CurrentSession.UserId;
            filter.Environment = this.Environment;
            favorites = this._service.LoadMarkedFunctions(filter);
            pagedFavorites = new PagerList<SiteItem>(filter.PageIndex, filter.PageSize, filter.TotalItemCount, favorites);

            return View(pagedFavorites);
        }
    }
}