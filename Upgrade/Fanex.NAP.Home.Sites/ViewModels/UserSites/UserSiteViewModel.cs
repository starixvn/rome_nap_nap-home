namespace Fanex.NAP.Home.Sites.ViewModels.UserSites;

public class UserSiteViewModel(IEnumerable<SiteItem> sites, int numberOfSitesPerRow)
{
    public int NumberOfSitesPerRow { get; init; } = numberOfSitesPerRow;

    public IEnumerable<SiteItem> Sites { get; init; } = sites;
}