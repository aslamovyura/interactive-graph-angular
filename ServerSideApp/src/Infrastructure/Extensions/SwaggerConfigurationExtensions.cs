using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ServerSideApp.Infrastructure.Extensions
{
    /// <summary>
    /// Define extension methods for Swagger service configuration.
    /// </summary>
    public static class SwaggerConfigurationExtensions
    {
        /// <summary>
        /// Add Swagger Service.
        /// </summary>
        /// <param name="services">DI container.</param>
        public static void AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Sales API",
                    Version = "v1",
                    Description = "Sales API. This is a Data-Driven/CRUD service."
                });
            });
        }
    }
}