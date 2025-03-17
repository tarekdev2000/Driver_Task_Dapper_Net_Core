using System.Reflection;

namespace DriverTask.Persistence
{
    using Common.General;
    //using Db;
    using DriverTask;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System.Data;

    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            var appOptions = configuration.GetSection(nameof(AppOptions)).Get<AppOptions>();


            services.AddScoped<IDbConnection>(sp => new SqliteConnection("Data Source=drivers.db"));
            services.AddScoped<IDriverRepository, DriverRepository>();

            return services;
        }
    }
}
