# Interactive Graph

![.NET Core](https://github.com/aslamovyura/interactive-graph-angular/workflows/.NET%20Core/badge.svg)

The main idea of application is to display sales statistics in the form of an interactive graph.
The application consists of the following 2 parts:

- [Server-side application](./ServerSideApp/README.md) built on .NET Core 3.1;
- [Client-side application](./ClientSideApp/README.md) built on Angular 9.

 Server-side application provides API for managing sales (simple CRUD operations) and for calculating sales statistics (number and amount of sales).
 Client-side application provide UI with interactive graph to contol sale statistics.

## Getting Started

### 1. Server-side app

To run the application, type the following commands from the `./ServerSideApp` directory:

```
> docker-compose build
> docker-compose up
```

For macOs or Linux systems, use `sudo` for commands above.
To know more about docker-compose, please visit [docker official site](https://docs.docker.com/compose/).

### 2. Client-side app

To install required packages and run the application, type the following commands from the `./ClientSideApp` directory:

```
> npm install
> npm start
```

## Built with

- [ASP.NET Core 3.1](https://docs.microsoft.com/en-us/aspnet/core/);
- [Amgular](https://angular.io/);
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

## License

This project is under the MIT License - see the [LICENSE.md](https://github.com/aslamovyura/interactive-graph-angular/blob/master/README.md) file for details.
