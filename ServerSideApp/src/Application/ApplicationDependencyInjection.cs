using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ServerSideApp.Application.Behaviors;
using System;
using System.Reflection;

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

            // Add mediatR and validation pipeline.
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            return services;
        }
    }
}