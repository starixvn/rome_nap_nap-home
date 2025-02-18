namespace Fanex.NAP.Home.Sites.Models.UserSites;
using System.Collections.Generic;

public class SitesInfo
{
    public IEnumerable<SiteUrlInfo> SiteUrlList { get; set; }

    public IEnumerable<MenuInfo> MenuList { get; set; }
}
