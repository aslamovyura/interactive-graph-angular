using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
        /// <param name="environment">Web hosting environment.</param>
        /// <returns>Services of Web layer.</returns>
        public static IServiceCollection AddWebServices(this IServiceCollection services,
                                                        IConfiguration configuration, 
                                                        IHostEnvironment environment)
        {

            var conntectionType = environment.IsProduction() ? "DockerConnection" : "DefaultConnection";

            var connectionString = configuration.GetConnectionString(conntectionType);
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddControllers();

            services.AddCors();
            services.AddHealthChecks();

            return services;
        }
    }
}