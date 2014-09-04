// --------------------------------------------------------------------------
// <copyright file="UserSitesService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.29">Create</change>
//   <change who="Jason.Ly" date="2014.9.1">
//      Call new SP (NAP_Home_MainPage), group function by first level parent functions.
//      Move CheckUserPermissionOnFunction and CheckUserPermissionOnSite from SiteBaseService.
//   </change>
// </history>

namespace Fanex.NAP.Home.Sites.Services
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Common;
    using Fanex.NAP.Framework.Command;
    using Fanex.NAP.Home.Sites.Criteria.UserSites;
    using Fanex.NAP.Home.Sites.Models.UserSites;
    using Fanex.NAP.Home.Sites.Repositories;
    using Fanex.NAP.Home.Sites.ViewModels.UserSites;

    /// <summary>
    /// User sites service.
    /// </summary>
    public class UserSitesService : IUserSitesService
    {
        private IUserSitesRepository _repository;

        public UserSitesService() :
            this(null)
        {
        }

        public UserSitesService(IUserSitesRepository repository)
        {
            this._repository = repository ?? new UserSitesRepository();
        }

        /// <summary>
        /// Gets accessible sites and functions of an user.
        /// </summary>
        /// <param name="filter">The filter object contains User ID and the Environment indicator.</param>
        /// <returns>List of sites and sites' functions that the user can access.</returns>
        public IEnumerable<SiteItem> LoadUserSitesAndFunctions(SearchFunctionsFilter filter)
        {
            var criteria = new GetSiteMenusByUserIdCriteria
            {
                UserId = filter.UserId,
                MenuItemName = filter.SearchedFunctionName,
                Environment = filter.Environment
            };
            IEnumerable<SiteItem> result = null;
            SitesInfo siteData = null;

            if (criteria.IsValid())
            {
                siteData = this._repository.GetSitesAndMenus(criteria);
            }

            if (siteData != null)
            {
                result = GroupUserSitesData(siteData, string.IsNullOrEmpty(filter.SearchedFunctionName));
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
            ICommandResult result = this._repository.CheckUserPermissionOnFunction(criteria);

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
            ICommandResult result = this._repository.CheckUserPermissionOnSite(criteria);

            return result;
        }

        /// <summary>
        /// Groups the user sites and functions data.
        /// </summary>
        /// <param name="siteData">The site data.</param>
        /// <param name="isLoadAll">Boolean indicator checks if current request is using to load all functions or not.</param>
        /// <returns>Data are grouped by sites.</returns>
        private IEnumerable<SiteItem> GroupUserSitesData(SitesInfo siteData, bool isLoadAll = false)
        {
            List<SiteItem> sites = new List<SiteItem>();

            if (siteData.SiteUrlList != null && siteData.SiteUrlList.Any())
            {
                IEnumerable<IGrouping<int?, SiteUrlInfo>> siteUrlGroups = siteData.SiteUrlList.GroupBy(site => site.SiteId);
                SiteUrlInfo siteInfo = null;
                List<string> siteLinks = null;
                List<MenuInfo> siteMenus = null;
                List<FunctionGroup> functionGroups = null;

                foreach (IGrouping<int?, SiteUrlInfo> siteUrlGroup in siteUrlGroups)
                {
                    siteInfo = siteUrlGroup.ElementAt(0);
                    siteLinks = siteUrlGroup.OrderBy(link => link.IsDefault).Select(link => link.SiteLink).ToList();
                    functionGroups = null;

                    if (siteData.MenuList != null && siteData.MenuList.Any(menu => menu.SiteId == siteUrlGroup.Key))
                    {
                        siteMenus = siteData.MenuList.Where(menu => menu.SiteId == siteUrlGroup.Key).ToList();
                        if (isLoadAll)
                        {
                            functionGroups = ConvertFunctionList(siteMenus);
                        }
                        else
                        {
                            functionGroups = siteMenus.Select(menu => new FunctionGroup(menu)).ToList();
                        }
                    }

                    sites.Add(new SiteItem(siteInfo, siteLinks, functionGroups));
                }
            }

            return sites;
        }

        /// <summary>
        /// Re-structures the functions into hierarchy and removes middle level functions.
        /// </summary>
        /// <param name="menus">List of menus queried from database.</param>
        /// <returns>List of FunctionGroup objects.</returns>
        private List<FunctionGroup> ConvertFunctionList(List<MenuInfo> menus)
        {
            List<FunctionGroup> functionGroups = null;

            if (menus != null && menus.Any())
            {
                List<FunctionTreeNode> functionTree = RestructureFunctions(menus);
                FunctionGroup functionGroup = null;
                List<FunctionItem> functionList = null;

                functionGroups = new List<FunctionGroup>();

                foreach (FunctionTreeNode node in functionTree)
                {
                    functionGroup = new FunctionGroup(node as FunctionItem);
                    functionList = new List<FunctionItem>();

                    if (node.ChildFunctionNodeList != null && node.ChildFunctionNodeList.Count > 0)
                    {
                        GetLeafNodeFunctions(node.ChildFunctionNodeList, ref functionList);
                        functionGroup.FunctionList = functionList;
                    }

                    functionGroups.Add(functionGroup);
                }
            }

            return functionGroups;
        }

        /// <summary>
        /// Restructures the menu data into function tree.
        /// </summary>
        /// <param name="menus">The menus data.</param>
        /// <param name="parentId">Parent ID.</param>
        /// <returns>List of FunctionTreeNode indicates the function tree.</returns>
        private List<FunctionTreeNode> RestructureFunctions(List<MenuInfo> menus, int parentId = 0)
        {
            var functionNodes = new List<FunctionTreeNode>();
            FunctionTreeNode functionNode = null;

            foreach (MenuInfo menu in menus)
            {
                if ((menu.ParentId == null && parentId == 0) || (menu.ParentId == parentId))
                {
                    functionNode = new FunctionTreeNode(menu);
                    functionNode.ChildFunctionNodeList.AddRange(RestructureFunctions(menus, Converter.ToInt(menu.MenuItemId)));
                    functionNodes.Add(functionNode);
                }
            }

            functionNodes = functionNodes.OrderBy(obj => obj.DisplayOrder).ToList();

            return functionNodes;
        }

        /// <summary>
        /// Removes the middle level functions and gets the leaf functions only.
        /// </summary>
        /// <param name="functionNodes">List of FunctionTreeNode objects represents the function tree.</param>
        /// <param name="functionList">List of child functions.</param>
        private void GetLeafNodeFunctions(List<FunctionTreeNode> functionNodes, ref List<FunctionItem> functionList)
        {
            functionList = functionList ?? new List<FunctionItem>();

            foreach (FunctionTreeNode node in functionNodes)
            {
                if (node.ChildFunctionNodeList != null && node.ChildFunctionNodeList.Count > 0)
                {
                    GetLeafNodeFunctions(node.ChildFunctionNodeList, ref functionList);
                }
                else
                {
                    functionList.Add(node as FunctionItem);
                }
            }
        }
    }
}