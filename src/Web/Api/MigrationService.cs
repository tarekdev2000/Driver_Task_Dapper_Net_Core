//using DriverTask.Persistence.Db;
using Microsoft.EntityFrameworkCore;

namespace DriverTask.Api
{
    using Dapper;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System;

    public interface IMigrationService
    {
        void ApplyMigrations();
    }

    public class MigrationService : IMigrationService
    {
        private readonly IServiceProvider _serviceProvider;

        public MigrationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ApplyMigrations()
        {
            using var scope = _serviceProvider.CreateScope();
 
            // Apply Migration
            try
            {
                ///
                using (var connection = scope.ServiceProvider.GetRequiredService<System.Data.IDbConnection>())
                {
                    connection.Execute(@"
                            CREATE TABLE IF NOT EXISTS Drivers (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                FirstName TEXT,
                                LastName TEXT,
                                Email TEXT,
                                PhoneNumber TEXT
                            )");
                }
            }
            catch (Exception ex)
            {
                // Log the error or handle it in some way
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while migrating the database.");
            }
        }
    }
}
