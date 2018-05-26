using Com.Ericmas001.DependencyInjection.Attributes;
using Com.Ericmas001.Logs.Enums;

namespace Com.Ericmas001.Logs.Services
{
    public class NoLoggerService : AbstractLoggerService
    {
        public override void Log(LogLevelEnum level, string message)
        {
            // Do nothing
        }
    }
}
