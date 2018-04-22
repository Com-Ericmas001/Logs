using Com.Ericmas001.Logs.Enums;
using Com.Ericmas001.Logs.Services.Interfaces;

namespace Com.Ericmas001.Logs.Services
{
    public class LoggerConfiguration : ILoggerConfiguration
    {
        public LogLevelEnum LogLevel { get; set; } = LogLevelEnum.Normal;
        public bool ShowTimestamp { get; set; } = true;
    }
}
