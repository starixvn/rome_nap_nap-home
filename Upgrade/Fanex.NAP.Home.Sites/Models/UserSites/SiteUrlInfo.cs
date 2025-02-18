namespace Fanex.NAP.Home.Sites.Models.UserSites;
public class SiteUrlInfo
{
    public int? SiteId { get; set; }

    public string SiteName { get; set; }

    public string SiteLink { get; set; }

    public string Environment { get; set; }

    public bool IsLocalLink {get;set;}

    public bool? IsDefault { get; set; }
}
