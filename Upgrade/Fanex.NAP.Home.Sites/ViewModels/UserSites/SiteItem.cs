namespace Fanex.NAP.Home.Sites.ViewModels.UserSites;
using System;
using System.Collections.Generic;
using System.Linq;
using Fanex.NAP.Home.Core.Helpers;
using Fanex.NAP.Home.Sites.Criteria.UserSites;
using Fanex.NAP.Home.Sites.Models;

[Serializable]
public class SiteItem
{
    public SiteItem(CreateSiteItemCriteria createSiteItemCriteria)
    {
        if (createSiteItemCriteria.SiteInfo != null && createSiteItemCriteria.SiteInfo.SiteId.HasValue)
        {
            SiteId = createSiteItemCriteria.SiteInfo.SiteId.Value;
            SiteName = createSiteItemCriteria.SiteInfo.SiteName;
            Thumbnail = SiteThumbnailsManager.GetImageName(SiteId);
        }

        FunctionGroupList = createSiteItemCriteria.FunctionGroups ?? new List<FunctionGroup>();
        if (createSiteItemCriteria.SiteLinks == null ||
            !createSiteItemCriteria.SiteLinks.Any())
        {
            return;
        }

        var firstLink = createSiteItemCriteria.SiteLinks.FirstOrDefault(link => link.IsLocalLink);
        if (createSiteItemCriteria.IsWebVpnAccess && firstLink != null && firstLink.IsLocalLink)
        {
            DefaultLink = StringExtensions.TrimOrEmpty(firstLink.SiteLink);
            OtherLinkList = null;
            return;
        }

        var nonLocalLinks = createSiteItemCriteria.SiteLinks.Where(siteLink => !siteLink.IsLocalLink).ToList();
        if (nonLocalLinks == null || !nonLocalLinks.Any())
        {
            DefaultLink = StringExtensions.TrimOrEmpty(firstLink.SiteLink);
            OtherLinkList = null;
            return;
        }

        DefaultLink = StringExtensions.TrimOrEmpty(nonLocalLinks[0].SiteLink);
        nonLocalLinks.RemoveAt(0);
        OtherLinkList = nonLocalLinks.Select(link => StringExtensions.TrimOrEmpty(link.SiteLink))
            .ToList();
    }

    public int SiteId { get; init; }

    public string SiteName { get; init; }

    public string DefaultLink { get; init; }

    public string Thumbnail { get; init; }

    public List<string> OtherLinkList { get; init; }

    public List<FunctionGroup> FunctionGroupList { get; init; }
}