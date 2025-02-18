namespace Fanex.NAP.Home.Sites.Criteria.UserSites;

using System.Collections.Generic;
using NAP.Home.Sites.Models.UserSites;
using NAP.Home.Sites.ViewModels.UserSites;

public class CreateSiteItemCriteria(SiteUrlInfo siteInfo, List<SiteUrlInfo> siteLinks, List<FunctionGroup> functionGroups, bool isWebVpnAccess)
{
    public SiteUrlInfo SiteInfo { get; set; } = siteInfo;

    public List<SiteUrlInfo> SiteLinks { get; set; } = siteLinks;

    public List<FunctionGroup> FunctionGroups { get; set; } = functionGroups;

    public bool IsWebVpnAccess { get; set; } = isWebVpnAccess;
}
