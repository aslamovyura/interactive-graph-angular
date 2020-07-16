﻿using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;
using ServerSideApp.Application.Settings;
using ServerSideApp.Infrastructure.Extensions;

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
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var dockerSettingSection = configuration.GetSection("EnvironmentSettings");
            var dockerSettings = dockerSettingSection.Get<EnvironmentSettings>();
            var isDockerSupport = dockerSettings.IsDockerSupport;

            var connectionString = configuration.GetConnectionString(isDockerSupport.ToDbConnectionString());

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