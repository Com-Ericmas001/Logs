using Com.Ericmas001.Logs.Enums;

namespace Com.Ericmas001.Logs.Services.Interfaces
{
    public interface ILoggerService
    {
        void Log(LogLevelEnum level, string message);
    }
}
