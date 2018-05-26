using System;
using System.Diagnostics;
using Com.Ericmas001.DependencyInjection.Attributes;
using Com.Ericmas001.Logs.Enums;
using Com.Ericmas001.Logs.Services.Interfaces;

namespace Com.Ericmas001.Logs.Services
{
    public class TraceLoggerService : AbstractLoggerService
    {
        private readonly ILoggerConfiguration m_LoggerConfiguration;

        public TraceLoggerService(ILoggerConfiguration loggerConfiguration)
        {
            m_LoggerConfiguration = loggerConfiguration;
        }

        public override void Log(LogLevelEnum level, string message)
        {
            string TimeStamp() => $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ";

            if (level <= m_LoggerConfiguration.MinimumLogLevel)
            {
                Trace.WriteLine($"{(m_LoggerConfiguration.ShowTimestamp ? TimeStamp() : string.Empty)}{message}");
            }

        }
    }
}
