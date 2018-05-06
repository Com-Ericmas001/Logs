using Com.Ericmas001.DependencyInjection.Registrants;
using Com.Ericmas001.Logs.LoggingDb.Services;
using Com.Ericmas001.Logs.LoggingDb.Services.Interfaces;

namespace Com.Ericmas001.Logs.LoggingDb
{
    public class LoggingDbRegistrant : AbstractRegistrant
    {
        private readonly string m_ConnString;

        public LoggingDbRegistrant(string connString)
        {
            m_ConnString = connString;
        }
        protected override void RegisterEverything()
        {
            Register<ILoggingDbContext, LoggingDbContext>(() => new LoggingDbContext(m_ConnString));

            Register<ILogWriterService, LogWriterService>();
            Register<ILogCleanerService, LogCleanerService>();

            AddToRegistrant<NoLogsRegistrant>();
        }
    }
}
