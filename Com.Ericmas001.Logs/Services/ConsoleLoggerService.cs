using System;
using Com.Ericmas001.Logs.Enums;
using Com.Ericmas001.Logs.Services.Interfaces;

namespace Com.Ericmas001.Logs.Services
{
    public class ConsoleLoggerService : AbstractLoggerService
    {
        private readonly ILoggerConfiguration _loggerConfiguration;

        public ConsoleLoggerService(ILoggerConfiguration loggerConfiguration)
        {
            _loggerConfiguration = loggerConfiguration;
        }

        public override void Log(LogLevelEnum level, string message)
        {
            string TimeStamp() => $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ";

            if (level <= _loggerConfiguration.MinimumLogLevel)
            {
                Console.WriteLine($"{(_loggerConfiguration.ShowTimestamp ? TimeStamp() : string.Empty)}{message}");
            }

        }
    }
}
