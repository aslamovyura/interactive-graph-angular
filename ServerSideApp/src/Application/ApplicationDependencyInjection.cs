using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Application
{
    /// <summary>
    /// Define extension methods to add services of Application layer.
    /// </summary>
    public static class ApplicationDependencyInjection
    {
        /// <summary>
        /// Add services of application layer.
        /// </summary>
        /// <param name="services">DI container.</param>
        /// <returns>Services collection.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}