// --------------------------------------------------------------------------
// <copyright file="SearchFunctionsService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.29">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.Services
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.NAP.Home.Sites.ViewModels.SearchFunctions;

    /// <summary>
    /// User sites service.
    /// </summary>
    public class SearchFunctionsService : SiteBaseService, ISearchFunctionsService
    {
        /// <summary>
        /// Gets all functions which satisfy search conditions.
        /// </summary>
        /// <param name="filter">The filter object contains searching conditions.</param>
        /// <returns>List of satisfied functions.</returns>
        public IEnumerable<SearchedFunctionItem> SearchFunctions(SearchingFunctionFilter filter)
        {
            this.QueryData(filter);

            return GetFunctions();
        }

        /// <summary>
        /// Creates sample date for User Site page.
        /// </summary>
        /// <returns>Sample data.</returns>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Temperately using.")]
        private List<SearchedFunctionItem> GetFunctions()
        {
            List<SearchedFunctionItem> functions = new List<SearchedFunctionItem>();

            functions.Add(new SearchedFunctionItem
            {
                SiteId = 1,
                FunctionId = 1,
                FunctionName = "Reporting Services",
                Link = "http://10.18.200.209:8007/nap-sc-pro-long-link",
                Path = "Reports » Fraud & Risk » Reporting Services"
            });

            functions.Add(new SearchedFunctionItem
            {
                SiteId = 1,
                FunctionId = 1,
                FunctionName = "Settlement Report",
                Link = "http://10.18.200.209:8007/nap-sc-pro-long-link",
                Path = "Reports » Fraud & Risk » Settlement Report"
            });

            functions.Add(new SearchedFunctionItem
            {
                SiteId = 1,
                FunctionId = 1,
                FunctionName = "Margin Report",
                Link = "http://10.18.200.209:8007/nap-sc-pro-long-link",
                Path = "Reports » Fraud & Risk » Margin Report"
            });

            functions.Add(new SearchedFunctionItem
            {
                SiteId = 1,
                FunctionId = 1,
                FunctionName = "Credit Term",
                Link = "http://10.18.200.209:8007/nap-sc-pro-long-link",
                Path = "Reports » Fraud & Risk » Credit Term"
            });

            functions.Add(new SearchedFunctionItem
            {
                SiteId = 1,
                FunctionId = 1,
                FunctionName = "Win Loss Win Loss Loss Win Summary",
                Link = "http://10.18.200.209:8007/nap-sc-pro-long-link",
                Path = "Reports » Fraud & Risk » Win Loss Summary"
            });

            functions.Add(new SearchedFunctionItem
            {
                SiteId = 1,
                FunctionId = 1,
                FunctionName = "Mix Parlay Win Loss Report",
                Link = "http://10.18.200.209:8007/nap-sc-pro-long-link",
                Path = "Reports » Fraud & Risk » Mix Parlay Win Loss Report"
            });

            functions.Add(new SearchedFunctionItem
            {
                SiteId = 1,
                FunctionId = 1,
                FunctionName = "Trader Performance Report",
                Link = "http://10.18.200.209:8007/nap-sc-pro-long-link",
                Path = "Reports » Fraud & Risk » Trader Performance Report"
            });

            functions.Add(new SearchedFunctionItem
            {
                SiteId = 1,
                FunctionId = 1,
                FunctionName = "Daily Win Loss (2h)(old) Report",
                Link = "http://10.18.200.209:8007/nap-sc-pro-long-link",
                Path = "Reports » Fraud & Risk » Daily Win Loss (2h)(old) Report"
            });

            functions.Add(new SearchedFunctionItem
            {
                SiteId = 1,
                FunctionId = 1,
                FunctionName = "Daily Win Loss (2h) Report",
                Link = "http://10.18.200.209:8007/nap-sc-pro-long-link",
                Path = "Reports » Fraud & Risk » Daily Win Loss (2h) Report"
            });

            functions.Add(new SearchedFunctionItem
            {
                SiteId = 1,
                FunctionId = 1,
                FunctionName = "Daily Win Loss (2a) Report",
                Link = "http://10.18.200.209:8007/nap-sc-pro-long-link",
                Path = "Reports » Fraud & Risk » Daily Win Loss (2a) Report"
            });

            return functions;
        }
    }
}