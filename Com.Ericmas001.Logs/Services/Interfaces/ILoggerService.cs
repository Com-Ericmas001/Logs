using Com.Ericmas001.Logs.Enums;

namespace Com.Ericmas001.Logs.Services.Interfaces
{
    public interface ILoggerService
    {
        void Log(string message);
        void LogError(string message);
        void LogImportant(string message);
        void LogInformation(string message);
        void LogVerbose(string message);
        void Log(LogLevelEnum level, string message);
    }
}
