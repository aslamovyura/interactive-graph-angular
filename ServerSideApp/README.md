# Interactive Graph (server-side application)

![.NET Core](https://github.com/aslamovyura/interactive-graph-angular/workflows/.NET%20Core/badge.svg)

This is a back-end part of the application, that displays sales statistics in the form of interactive graph.
The current service provides and API for managing sales (simple CRUD operations) and for calculating sales statistics (number and amount of sales).

## Getting Started

### Configuration

Configure application by editing the following `appsettings.json` file in the `./src/Web` directory:
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=127.0.0.1,1433;Database=SalesDb;User Id=sa;Password=reallyStrongPwd123",
    "DockerConnection": "Server=sql_server;Database=SalesDb;User Id=sa;Password=reallyStrongPwd123;"
  },

  "InitialDbSeedEnable": true,
  "AllowedHosts": "*"
}
```

Database initial seed is ENABLED by default. To disable database seeding with initial sales data, set `InitialDbSeedEnable` to `false`.

### Run application

To run the application, type the following commands from the app root directory:

```
> docker-compose build
> docker-compose up
```

For macOs or Linux systems, use `sudo` for commands above.
To know more about docker-compose, please visit [docker official site](https://docs.docker.com/compose/).

For more information on the service API, use the Swagger service by navigating to the following URL:

```
http://localhost:3000/swagger
```

## Built with

- [ASP.NET Core 3.1](https://docs.microsoft.com/en-us/aspnet/core/);
- [Clean architecture](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures);
- [REST API](https://restfulapi.net/) with [Swagger](https://swagger.io/);
- [CRUD](https://docs.microsoft.com/en-us/iis-administration/api/crud);
- [Docker](https://www.docker.com/);
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) for [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-2019);
- [CQRS](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs) with [Fluent Validation](https://fluentvalidation.net/),
- [Automapper](https://automapper.org/);
- [Health check](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-3.1);
- Logging with [Serilog](https://serilog.net/);
- Unit tests with [xUnit](https://xunit.net/), [Moq](https://github.com/Moq/moq4/wiki/Quickstart) and [Shouldly](https://github.com/shouldly/shouldly).

## Author

[Yury Aslamov](https://aslamovyura.github.io/) - Software Developer, Ph.D.