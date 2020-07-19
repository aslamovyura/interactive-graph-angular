using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using ServerSideApp.Infrastructure.Persistence;
using ServerSideApp.Web.Constants;
using System;

namespace ServerSideApplication.Web.Extensions
{
    /// <summary>
    /// Define context seed.
    /// </summary>
    public class ApplicationContextSeed
    {
        /// <summary>
        /// Seed database with initial data.
        /// </summary>
        /// <param name="serviceProvider">Service provider.</param>
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var initialDbSeedEnable = Convert.ToBoolean(configuration.GetSection("InitialDbSeedEnable").Value);
            if (!initialDbSeedEnable)
            {
                Log.Information(InitializationConstants.SeedDisabled);
                return;
            }

            try
            {
                Log.Information(InitializationConstants.SeedEnabled);

                var contextOptions = serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>();

                using var applicationContext = new ApplicationDbContext(contextOptions);
                ApplicationDbContextSeeder.SeedAsync(applicationContext).GetAwaiter().GetResult();

                Log.Information(InitializationConstants.SeedSuccess);
            }
            catch (Exception ex)
            {
                Log.Error(ex, InitializationConstants.SeedError);
            }
        }
    }
}