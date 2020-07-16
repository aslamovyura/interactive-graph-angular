using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServerSideApp.Infrastructure.Persistence;
using ServerSideApp.Web.Constants;
using System;
using Serilog;

namespace ServerSideApp.Web.Extensions
{
    public class RuntimeMigrations
    {
        /// <summary>
        /// Implement runtime migrations.
        /// </summary>
        /// <param name="serviceProvider">Services provider.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Initialize(IServiceProvider serviceProvider)
        {
            serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            try
            {
                var appContextService = serviceProvider.GetRequiredService<ApplicationDbContext>();
                appContextService.Database.Migrate();

                Log.Information(InitializationConstants.MigrationSuccess);
            }
            catch (Exception ex)
            {
                Log.Error(ex, InitializationConstants.MigrationError);
            }
        }
    }
}