using System;
using System.Linq;
using Com.Ericmas001.Logs.Enums;
using Com.Ericmas001.Logs.LoggingDb.Services.Interfaces;
using Com.Ericmas001.Logs.Services.Interfaces;

namespace Com.Ericmas001.Logs.LoggingDb.Services
{
    public class LogCleanerService : ILogCleanerService
    {
        private readonly ILoggingDbContext _logDbContext;
        private readonly ILoggerService _executionLogService;

        public LogCleanerService(ILoggingDbContext logDbContext, ILoggerService executionLogService)
        {
            _logDbContext = logDbContext;
            _executionLogService = executionLogService;
        }

        public int RemoveLogsOlderThan(DateTime minDate)
        {
            _logDbContext.SetCommandTimeout(3600);

            _executionLogService.Log(LogLevelEnum.Information, $"=================================================================");
            _executionLogService.Log(LogLevelEnum.Information, $"Deleting logs older than {minDate:yyyy-MM-dd HH:mm:ss}");
            var resultsInRange = _logDbContext.ExecutedCommands.Where(x => x.ExecutedTime < minDate).Select(x => x.IdExecutedCommand).Take(50).ToArray();
            var treated = 0;
            while (resultsInRange.Any())
            {
                var nbResultsInRange = resultsInRange.Length;

                foreach (var c in resultsInRange)
                {
                    var entity = _logDbContext.ExecutedCommands.Find(c);
                    _logDbContext.ExecutedCommands.Remove(entity);
                }

                treated += nbResultsInRange;
                _executionLogService.Log(LogLevelEnum.Information, $"{nbResultsInRange} log entries deleted ! Total : {treated}");
                _logDbContext.SaveChanges();

                resultsInRange = _logDbContext.ExecutedCommands.Where(x => x.ExecutedTime < minDate).Select(x => x.IdExecutedCommand).Take(50).ToArray();
            }

            _logDbContext.SaveChanges();
            _executionLogService.Log(LogLevelEnum.Information, $"All The logs were deleted successfully !!");

            _executionLogService.Log(LogLevelEnum.Information, $"=================================================================");
            return treated;
        }

        public int RemoveUnusedClients()
        {
            _logDbContext.SetCommandTimeout(3600);
            var clientsToRemove = _logDbContext.Clients.Where(x => !x.ExecutedCommands.Any()).ToArray();

            foreach (var c in clientsToRemove)
                _logDbContext.Clients.Remove(c);

            _logDbContext.SaveChanges();

            return clientsToRemove.Length;
        }

        public int RemoveUnusedServices()
        {
            _logDbContext.SetCommandTimeout(3600);
            var servicesToRemove = _logDbContext.ServiceMethods.Where(x => !x.ExecutedCommands.Any()).ToArray();

            foreach (var s in servicesToRemove)
                _logDbContext.ServiceMethods.Remove(s);

            _logDbContext.SaveChanges();

            return servicesToRemove.Length;
        }
    }
}
