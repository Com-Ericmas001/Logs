using System;

namespace Com.Ericmas001.Logs.LoggingDb.Services.Interfaces
{
    public interface ILogCleanerService
    {
        int RemoveLogsOlderThan(DateTime minDate);
        int RemoveUnusedClients();
        int RemoveUnusedServices();
    }
}
