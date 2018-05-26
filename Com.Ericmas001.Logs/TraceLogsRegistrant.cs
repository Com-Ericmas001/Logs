using Com.Ericmas001.DependencyInjection.Attributes;
using Com.Ericmas001.DependencyInjection.Registrants;
using Com.Ericmas001.Logs.Enums;
using Com.Ericmas001.Logs.Services;
using Com.Ericmas001.Logs.Services.Interfaces;

namespace Com.Ericmas001.Logs
{
    [ManualRegistrant]
    public class TraceLogsRegistrant : AbstractRegistrant
    {
        protected override void RegisterEverything()
        {
            RegisterInstance<ILoggerConfiguration>(new LoggerConfiguration{MinimumLogLevel = LogLevelEnum.Verbose});
            Register<ILoggerService, TraceLoggerService>();
        }
    }
}
