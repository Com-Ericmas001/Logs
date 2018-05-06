using Com.Ericmas001.Logs.LoggingDb.Entities;
using Microsoft.EntityFrameworkCore;

namespace Com.Ericmas001.Logs.LoggingDb
{
    public interface ILoggingDbContext
    {
        DbSet<Client> Clients { get; set; }
        DbSet<ExecutedCommand> ExecutedCommands { get; set; }
        DbSet<ServiceMethod> ServiceMethods { get; set; }
        DbSet<SentNotification> SentNotifications { get; set; }

        int SaveChanges();
        void SetCommandTimeout(int value);
    }
}
