namespace Fanex.NAP.Home.Sites.ViewModels.UserSites;
using System;
using System.Collections.Generic;
using System.Linq;
using Fanex.NAP.Home.Core.Helpers;
using Fanex.NAP.Home.Sites.Criteria.UserSites;
using Fanex.NAP.Home.Sites.Models;
using NAP.Home.Sites.Models.UserSites;

[Serializable]
public class SiteItem
{
    public SiteItem(CreateSiteItemCriteria createSiteItemCriteria)
    {
        InitializeSiteInfo(createSiteItemCriteria);
        InitializeFunctionGroups(createSiteItemCriteria);
        InitializeLinks(createSiteItemCriteria);
    }

    private void InitializeSiteInfo(CreateSiteItemCriteria createSiteItemCriteria)
    {
        if (createSiteItemCriteria.SiteInfo?.SiteId == null)
        {
            return;
        }
        
        SiteId = createSiteItemCriteria.SiteInfo.SiteId.Value;
        SiteName = createSiteItemCriteria.SiteInfo.SiteName;
        Thumbnail = SiteThumbnailsManager.GetImageName(SiteId);
    }

    private void InitializeFunctionGroups(CreateSiteItemCriteria createSiteItemCriteria)
    {
        FunctionGroupList = createSiteItemCriteria.FunctionGroups ?? new List<FunctionGroup>();
    }

    private void InitializeLinks(CreateSiteItemCriteria createSiteItemCriteria)
    {
        if (createSiteItemCriteria.SiteLinks?.Any() != true)
        {
            return;
        }

        var firstLink = createSiteItemCriteria.SiteLinks.Find(link => link.IsLocalLink);
        
        if (ShouldUseLocalLink(createSiteItemCriteria, firstLink))
        {
            SetDefaultLink(firstLink);
            return;
        }

        var nonLocalLinks = createSiteItemCriteria.SiteLinks.Where(siteLink => !siteLink.IsLocalLink).ToList();
        
        if (!nonLocalLinks.Any())
        {
            SetDefaultLink(firstLink);
            return;
        }

        SetLinksFromNonLocal(nonLocalLinks);
    }

    private static bool ShouldUseLocalLink(CreateSiteItemCriteria createSiteItemCriteria, SiteUrlInfo? firstLink)
    {
        return createSiteItemCriteria.IsWebVpnAccess && firstLink?.IsLocalLink == true;
    }

    private void SetDefaultLink(SiteUrlInfo? link)
    {
        DefaultLink = StringExtensions.TrimOrEmpty(link?.SiteLink);
        OtherLinkList = null;
    }

    private void SetLinksFromNonLocal(List<SiteUrlInfo> nonLocalLinks)
    {
        DefaultLink = StringExtensions.TrimOrEmpty(nonLocalLinks[0].SiteLink);
        nonLocalLinks.RemoveAt(0);
        OtherLinkList = nonLocalLinks.Select(link => StringExtensions.TrimOrEmpty(link.SiteLink)).ToList();
    }

    public int SiteId { get; set; }

    public string SiteName { get; set; }

    public string DefaultLink { get; set; }

    public string Thumbnail { get; set; }

    public List<string> OtherLinkList { get; set; }

    public List<FunctionGroup> FunctionGroupList { get; set; }
}