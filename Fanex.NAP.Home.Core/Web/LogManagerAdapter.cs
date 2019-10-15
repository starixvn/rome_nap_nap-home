using Fanex.Logging;

namespace Fanex.NAP.Home.Core.Web
{
    internal class LogManagerAdapter : LogManager
    {
        public new virtual LogManager SetDefaultLogCategory(string defaultCategory)
            => LogManager.SetDefaultLogCategory(defaultCategory);
    }
}
