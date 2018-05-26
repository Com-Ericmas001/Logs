using System;
using Com.Ericmas001.Logs.Enums;
using Com.Ericmas001.Logs.Services.Interfaces;

namespace Com.Ericmas001.Logs.Services
{
    public class ConsoleLoggerService : AbstractLoggerService
    {
        private readonly ILoggerConfiguration m_LoggerConfiguration;

        public ConsoleLoggerService(ILoggerConfiguration loggerConfiguration)
        {
            m_LoggerConfiguration = loggerConfiguration;
        }

        public override void Log(LogLevelEnum level, string message)
        {
            string TimeStamp() => $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ";

            if (level <= m_LoggerConfiguration.MinimumLogLevel)
            {
                Console.WriteLine($"{(m_LoggerConfiguration.ShowTimestamp ? TimeStamp() : string.Empty)}{message}");
            }

        }
    }
}
