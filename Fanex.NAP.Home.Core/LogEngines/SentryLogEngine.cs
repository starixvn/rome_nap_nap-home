using Fanex.Logging;
using Fanex.Logging.Sentry.RavenClient;
using Fanex.NAP.Home.Core.Helpers;

namespace Fanex.NAP.Home.Core.LogEngines
{
    internal class SentryLogEngine : LogEngineFactory
    {
        public ILogEngine Create()
        {
            var sentryOptions = new SentryEngineOptions
            {
                Dsn = new SharpRaven.Dsn(AppSettings.SentryBackendUrl),
                FrontEndDsn = new SharpRaven.Dsn(AppSettings.SentryUrl)
            };

            return new SentryLogging(sentryOptions, AppSettings.RavenLibPath);
        }
    }
}
