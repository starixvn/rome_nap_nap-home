using Fanex.Logging;

namespace Fanex.NAP.Home.Core.LogEngines
{
    internal interface LogEngineFactory
    {
        ILogEngine Create();
    }
}
