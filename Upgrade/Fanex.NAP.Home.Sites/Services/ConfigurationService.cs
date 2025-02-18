namespace Fanex.NAP.Home.Sites.Services;

using Microsoft.Extensions.Configuration;
public interface ConfigurationService
{
    List<string> ShowSiteEnvironments { get; }
    string UMToken { get; }

    string CurrentDirectory { get; }

    int NumberOfSitesPerRow { get; }

    string Environment { get; }
}

public class ConfigurationServiceImp(IConfiguration configuration) : ConfigurationService
{
    public List<string> ShowSiteEnvironments => configuration.GetValue<string>("ShowSiteEnvironments").Split(",").ToList();

    public string UMToken => configuration.GetValue<string>("UMToken");

    public string CurrentDirectory => AppDomain.CurrentDomain.BaseDirectory;

    public int NumberOfSitesPerRow => configuration.GetValue<int>("NumberOfSitesPerRow");

    public string Environment => configuration.GetValue<string>("Environment");
}
