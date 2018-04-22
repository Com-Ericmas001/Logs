using Com.Ericmas001.DependencyInjection.Registrants;
using Com.Ericmas001.Logs.Services;
using Com.Ericmas001.Logs.Services.Interfaces;

namespace Com.Ericmas001.Logs
{
    public class ConsoleLogsRegistrant : AbstractRegistrant
    {
        protected override void RegisterEverything()
        {
            RegisterInstance<ILoggerConfiguration>(new LoggerConfiguration());
            Register<ILoggerService, ConsoleLoggerService>();
        }
    }
}
