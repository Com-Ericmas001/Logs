using Com.Ericmas001.Logs.Enums;
using Com.Ericmas001.Logs.Services.Interfaces;

namespace Com.Ericmas001.Logs.Services
{
    public class NoLoggerService : ILoggerService
    {
        public void Log(LogLevelEnum level, string message)
        {
            // Do nothing
        }
    }
}
