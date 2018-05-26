using Com.Ericmas001.Logs.Enums;
using Com.Ericmas001.Logs.Services.Interfaces;

namespace Com.Ericmas001.Logs.Services
{
    public abstract class AbstractLoggerService : ILoggerService
    {
        public void Log(string message)
        {
            Log(LogLevelEnum.Normal, message);
        }

        public void LogError(string message)
        {
            Log(LogLevelEnum.Error, message);
        }

        public void LogImportant(string message)
        {
            Log(LogLevelEnum.Important, message);
        }

        public void LogInformation(string message)
        {
            Log(LogLevelEnum.Information, message);
        }

        public void LogVerbose(string message)
        {
            Log(LogLevelEnum.Verbose, message);
        }

        public abstract void Log(LogLevelEnum level, string message);
    }
}
