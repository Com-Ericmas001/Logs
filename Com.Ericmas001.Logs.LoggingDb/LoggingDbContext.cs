using System;
using Com.Ericmas001.Logs.LoggingDb.Entities;
using Microsoft.EntityFrameworkCore;

namespace Com.Ericmas001.Logs.LoggingDb
{
    public class LoggingDbContext : DbContext, ILoggingDbContext
    {
        private readonly string m_ConnString;

        public LoggingDbContext(string connString)
        {
            m_ConnString = connString;
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<ExecutedCommand> ExecutedCommands { get; set; }
        public virtual DbSet<ServiceMethod> ServiceMethods { get; set; }
        public virtual DbSet<SentNotification> SentNotifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(m_ConnString);
            }
        }
        public void SetCommandTimeout(int value)
        {
            Database.SetCommandTimeout(value);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasMany(e => e.ExecutedCommands)
                .WithOne(e => e.Client)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ServiceMethod>()
                .HasMany(e => e.ExecutedCommands)
                .WithOne(e => e.ServiceMethod)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
