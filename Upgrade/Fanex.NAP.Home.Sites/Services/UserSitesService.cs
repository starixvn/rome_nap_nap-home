namespace Fanex.NAP.Home.Sites.Services;

using System.Collections.Generic;
using System.Linq;
using System;
using Fanex.NAP.Home.Sites.Models;
using Fanex.NAP.Home.Sites.Criteria.UserSites;
using Fanex.NAP.Home.Sites.Models.UserSites;
using Fanex.NAP.Home.Sites.ViewModels.UserSites;
using Fanex.Data.Repository;

public interface IUserSitesService
{
    IEnumerable<SiteItem> LoadUserSitesAndFunctions(SearchFunctionsFilter filter);

    CommandResult CheckUserPermissionOnFunction(ForwardFilter filter);

    CommandResult CheckUserPermissionOnSite(ForwardFilter filter);
}

public class UserSitesService(IDynamicRepository repository, ConfigurationService configuration, IPHelper ipHelper) : IUserSitesService
{
    private readonly IDynamicRepository _repository = repository;
    private readonly ConfigurationService configuration = configuration;
    private readonly IPHelper ipHelper = ipHelper;

    public IEnumerable<SiteItem> LoadUserSitesAndFunctions(SearchFunctionsFilter filter)
    {
        var criteria = new GetSiteMenusByUserIdCriteria(filter.UserId, filter.Environment, filter.SearchedFunctionName);
        IEnumerable<SiteItem> result = null;
        var siteData = new SitesInfo();

        var siteData1 = _repository.FetchMultiple<SiteUrlInfo, MenuInfo>(criteria);
        siteData.SiteUrlList = siteData1.Item1;
        siteData.MenuList = siteData1.Item2;
        siteData.SiteUrlList = siteData.SiteUrlList
        .Select(i =>
        {
            i.IsLocalLink = ipHelper.IsLocalLink(i.SiteLink);
            return i;
        })
        .Where(s => configuration.ShowSiteEnvironments.Any(se => se.Equals(s.Environment, StringComparison.CurrentCultureIgnoreCase))).ToList();
        if (siteData != null)
        {
            result = GroupUserSitesData(siteData, string.IsNullOrEmpty(filter.SearchedFunctionName));
        }

        return result;
    }

    public CommandResult CheckUserPermissionOnFunction(ForwardFilter filter)
    {
        var criteria = new CheckPermissionByMenuUserIdCriteria(filter.UserId, filter.SiteId, filter.FunctionId.Value, filter.Environment);
        _repository.Execute(criteria);
        return new CommandResult(criteria.ErrCode, criteria.ErrMsg);
    }

    public CommandResult CheckUserPermissionOnSite(ForwardFilter filter)
    {
        var criteria = new CheckUserPermissionBySiteCriteria(filter.UserId, filter.SiteId, filter.Environment);

        _repository.Execute(criteria);
        return new CommandResult(criteria.ErrCode, criteria.ErrMsg);
    }

    private IEnumerable<SiteItem> GroupUserSitesData(SitesInfo siteData, bool isLoadAll = false)
    {
        var sites = new List<SiteItem>();
        if (siteData.SiteUrlList == null || !siteData.SiteUrlList.Any())
        {
            return sites;
        }

        IEnumerable<IGrouping<int?, SiteUrlInfo>> siteUrlGroups =
            siteData.SiteUrlList.GroupBy(site => site.SiteId);
        List<MenuInfo> siteMenus;
        foreach (IGrouping<int?, SiteUrlInfo> siteUrlGroup in siteUrlGroups)
        {
            var createSiteItemCriteria = new CreateSiteItemCriteria(siteUrlGroup.ElementAt(0),
                siteUrlGroup.OrderByDescending(link => link.IsDefault)
                    .OrderByDescending(link => link.IsLocalLink)
                    .ToList(),
                null, ipHelper.IsWebVpnAccess());

            if (siteData.MenuList != null &&
                siteData.MenuList.Any(menu => menu.SiteId == siteUrlGroup.Key))
            {
                siteMenus = siteData.MenuList.Where(menu => menu.SiteId == siteUrlGroup.Key).ToList();
                createSiteItemCriteria.FunctionGroups = isLoadAll ?
                    ConvertFunctionList(siteMenus) :
                    siteMenus.Select(menu => new FunctionGroup(menu)).ToList();
            }

            sites.Add(new SiteItem(createSiteItemCriteria));
        }

        return sites;
    }

    private List<FunctionGroup> ConvertFunctionList(List<MenuInfo> menus)
    {
        List<FunctionGroup> functionGroups = null;

        if (menus != null && menus.Any())
        {
            List<FunctionTreeNode> functionTree = RestructureFunctions(menus);
            FunctionGroup functionGroup;
            List<FunctionItem> functionList;

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

    private List<FunctionTreeNode> RestructureFunctions(List<MenuInfo> menus, int parentId = 0)
    {
        var functionNodes = new List<FunctionTreeNode>();
        FunctionTreeNode functionNode;

        foreach (MenuInfo menu in menus)
        {
            if ((menu.ParentId == null && parentId == 0) || (menu.ParentId == parentId))
            {
                functionNode = new FunctionTreeNode(menu);
                functionNode.ChildFunctionNodeList.AddRange(RestructureFunctions(menus, menu.MenuItemId.Value));
                functionNodes.Add(functionNode);
            }
        }

        functionNodes = functionNodes.OrderBy(obj => obj.DisplayOrder).ToList();

        return functionNodes;
    }

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
