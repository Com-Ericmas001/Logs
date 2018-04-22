using Com.Ericmas001.Logs.Enums;

namespace Com.Ericmas001.Logs.Services.Interfaces
{
    public interface ILoggerConfiguration
    {
        LogLevelEnum LogLevel { get; }
        bool ShowTimestamp { get; }
    }
}
