using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServerSideApp.Application.Interfaces;
using ServerSideApp.Infrastructure.Persistence;

namespace ServerSideApp.Web
{
    /// <summary>
    /// Define extension methods to add services of Web layer.
    /// </summary>
    public static class WebDependencyInjection
    {
        /// <summary>
        /// Add services of Web layer.
        /// </summary>
        /// <param name="services">DI container.</param>
        /// <param name="configuration">Application configuration.</param>
        /// <returns>Services of Web layer.</returns>
        public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("Infrastructure")));

            services.AddControllers();

            services.AddHealthChecks();
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            return services;
        }
    }
}