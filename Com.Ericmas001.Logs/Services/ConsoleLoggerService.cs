using System;
using Com.Ericmas001.Logs.Enums;
using Com.Ericmas001.Logs.Services.Interfaces;

namespace Com.Ericmas001.Logs.Services
{
    public class ConsoleLoggerService : ILoggerService
    {
        private readonly ILoggerConfiguration m_LoggerConfiguration;

        public ConsoleLoggerService(ILoggerConfiguration loggerConfiguration)
        {
            m_LoggerConfiguration = loggerConfiguration;
        }

        public void Log(LogLevelEnum level, string message)
        {
            string TimeStamp() => $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ";

            if (level >= m_LoggerConfiguration.LogLevel)
            {
                Console.WriteLine($"{(m_LoggerConfiguration.ShowTimestamp ? TimeStamp() : string.Empty)}{message}");
            }

        }
    }
}
