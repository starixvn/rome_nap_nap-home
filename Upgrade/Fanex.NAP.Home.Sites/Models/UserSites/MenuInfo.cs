namespace Fanex.NAP.Home.Sites.Models.UserSites;
public class MenuInfo
{
    public int? SiteId { get; set; }

    public int? MenuItemId { get; set; }

    public string MenuItemName { get; set; }

    public int? ParentId { get; set; }

    public string MenuLink { get; set; }

    public short? DisplayOrder { get; set; }
}
