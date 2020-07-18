using Microsoft.Extensions.DependencyInjection;
using ServerSideApp.Application.Interfaces;
using ServerSideApp.Infrastructure.Extensions;
using ServerSideApp.Infrastructure.Persistence;
using ServerSideApp.Infrastructure.Services;

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
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddScoped<ISaleStatisticService, SaleStatisticService>();
            services.AddSwaggerService();

            return services;
        }
    }
}