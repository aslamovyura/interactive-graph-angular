using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;
using System;

namespace ServerSideApp.Infrastructure.Logging
{
    /// <summary>
    /// Define logging configuration.
    /// </summary>
    public class SerilogConfiguration
    {
        /// <summary>
        /// Define serilog configuration.
        /// </summary>
        /// <returns>Serilog configuration.</returns>
        public static Logger LoggerConfig()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var isProduction = environment == Environments.Production;

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionType = isProduction ? "DockerConnection" : "DefaultConnection";
            var connectionString = configuration[$"ConnectionStrings:{connectionType}"];

            var serilogConfig =
                new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.MSSqlServer(connectionString: connectionString,
                                     sinkOptions: new SinkOptions
                                     {
                                         TableName = "Serilog",
                                         AutoCreateSqlTable = true
                                     })

                .CreateLogger();

            return serilogConfig;
        }
    }
}