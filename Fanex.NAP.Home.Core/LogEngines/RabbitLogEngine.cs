using Fanex.Logging;
using Fanex.Logging.RabbitMQ;
using Fanex.NAP.Home.Core.Helpers;

namespace Fanex.NAP.Home.Core.LogEngines
{
    internal class RabbitLogEngine : LogEngineFactory
    {
        public ILogEngine Create()
            => new RabbitMQLogging(AppSettings.RabbitMQConnectionStrings);
    }
}
