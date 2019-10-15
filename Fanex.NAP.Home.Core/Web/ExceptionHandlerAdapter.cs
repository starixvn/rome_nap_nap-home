using Fanex.Logging.Extensions.Web;

namespace Fanex.NAP.Home.Core.Web
{
    internal class ExceptionHandlerAdapter
    {
        public virtual LoggingConfiguration UseConfig(LoggingConfiguration loggingConfiguration)
            => ExceptionHandler.UseConfig(loggingConfiguration);
    }
}
