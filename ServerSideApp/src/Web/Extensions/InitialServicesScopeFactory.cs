using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServerSideApplication.Web.Extensions;
using System;

namespace ServerSideApp.Web.Extensions
{
    /// <summary>
    /// Define initial services scope factory.
    /// </summary>
    public class InitialServicesScopeFactory
    {
        /// <summary>
        /// Build services factory.
        /// </summary>
        /// <param name="host">Application Host.</param>
        public static void Build(IHost host)
        {
            host = host ?? throw new ArgumentNullException(nameof(host));

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            RuntimeMigrations.Initialize(services);
            ApplicationContextSeed.Initialize(services);
        }
    }
}