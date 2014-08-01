// --------------------------------------------------------------------------
// <copyright file="SearchFunctionsController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.28">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Fanex.NAP.Core.Web.Attribute;
    using Fanex.NAP.Home.Core.Helpers;
    using Fanex.NAP.Home.Sites.Services;
    using Fanex.NAP.Home.Sites.ViewModels.SearchFunctions;

    /// <summary>
    /// Search sites controller.
    /// </summary>
    [NapSuperAuth]
    public class SearchFunctionsController : SiteBaseController
    {
        private ISearchFunctionsService _service;

        public SearchFunctionsController() :
            this(null)
        {
        }

        public SearchFunctionsController(ISearchFunctionsService service)
        {
            this._service = service ?? new SearchFunctionsService();
        }

        /// <summary>
        /// Redirect to Search page.
        /// </summary>
        /// <param name="filter">Filter object contains searching conditions.</param>
        /// <returns>Return Search page.</returns>
        [HttpGet]
        public ActionResult Index(SearchingFunctionFilter filter)
        {
            IEnumerable<SearchedFunctionItem> searchedFunctions = null;
            PagerList<SearchedFunctionItem> pagedFunctions = null;

            ViewBag.SearchedFunctionName = filter.FunctionName;
            ViewBag.CurrentPageIndex = filter.PageIndex;
            filter.PageSize = AppSettings.SearchPageSize;
            filter.UserId = this.CurrentSession.UserId;
            filter.Environment = this.Environment;
            searchedFunctions = this._service.SearchFunctions(filter);
            pagedFunctions = new PagerList<SearchedFunctionItem>(filter.PageIndex, filter.PageSize, filter.TotalItemCount, searchedFunctions);

            return View(pagedFunctions);
        }
    }
}