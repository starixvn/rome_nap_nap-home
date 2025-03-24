using Microsoft.Extensions.Configuration;

namespace Configurations
{
    public interface ConfigurationService
    {
        string FraudSystemCategoryApi { get; }

        bool IsDisableMostvisitedPage { get; }
    }

    public class ConfigurationServiceImp(IConfiguration configuration) : ConfigurationService
    {
        public string FraudSystemCategoryApi => configuration["FraudSystemCategoryAPI"];

        public bool IsDisableMostvisitedPage => configuration.GetValue<bool>("IsDisableMostvisitedPage");
    }
}
