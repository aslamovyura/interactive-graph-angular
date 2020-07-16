using Microsoft.Extensions.DependencyInjection;

namespace ServerSideApp.Infrastructure
{
    /// <summary>
    /// Define extention method to add services of Infrastructure layer.
    /// </summary>
    public static class InfrastructureDependencyInjection
    {
        /// <summary>
        /// Add services of the Infrastrucure layer.
        /// </summary>
        /// <param name="services">DI container.</param>
        /// <returns>Services.</returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // TODO: add infrastructure services.
            return services;
        }
    }
}