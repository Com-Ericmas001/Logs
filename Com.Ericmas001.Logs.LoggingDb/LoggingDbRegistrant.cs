using System.Collections.Generic;
using Com.Ericmas001.DependencyInjection.Registrants;
using Com.Ericmas001.DependencyInjection.Registrants.Interfaces;
using Com.Ericmas001.Logs.LoggingDb.Services;
using Com.Ericmas001.Logs.LoggingDb.Services.Interfaces;

namespace Com.Ericmas001.Logs.LoggingDb
{
    public class LoggingDbRegistrant : AbstractRegistrant, IConnectionStringRegistrant
    {
        public Dictionary<string, string> ConnectionStrings { get; set; }
        protected override void RegisterEverything()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Register<ILoggingDbContext, LoggingDbContext>(() => new LoggingDbContext(ConnectionStrings[GetType().Namespace]));

            Register<ILogWriterService, LogWriterService>();
            Register<ILogCleanerService, LogCleanerService>();

            AddToRegistrant<NoLogsRegistrant>();
        }
    }
}
