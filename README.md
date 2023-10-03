# BACKEND TASTEAL REST API .NET CORE 7

Backend code for a university project.

## How to use

To get started, you need to:

- install MySQL on your machine
- replace ConnectionString to this configuration in your appsettings.json:

```
"ConnectionStrings": {
  "DefaultString": "Server=(localdb)\\mssqllocaldb;Database=ApiTemplate;Trusted_Connection=True;"
}
```

- run the application using Visual Studio 2022 or the .NET CLI

## Technologies

This project uses the following technologies:

- [.NET 7](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-7)
- C#
- [MYSQL](https://www.mysql.com) for developing and production
- [Entity Framework Core 7](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-7.0/whatsnew)
- [Dapper](https://github.com/DapperLib/Dapper)
- [Serilog](https://serilog.net/)
- [Seq](https://datalust.co/seq) or Elastic Search
- [Automapper](https://automapper.org/)
- [Swagger](https://swagger.io/)
- [Global Error Middleware](https://code-maze.com/global-error-handling-aspnetcore/)
- [Repository Pattern](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design)
- Validation Filter Attribute to validate DTOs
- [API Versioning](https://github.com/dotnet/aspnet-api-versioning/wiki) and improved swagger documentation to support multiple versions

## Project Structure

![3layer-model](https://github.com/NguyenMinhHieu3101/BE_tasteal/assets/105068972/4a5dd692-36d5-43fe-b281-56a2d4f5a410)

- `API`: This folder contains the source code for the web API.
  - `Controllers`: This folder contains the controllers that handle incoming requests.
  - `Program.c	s`: This file is responsible for configuring and running the web host. After .NET6 we don't have to use Startup.cs anymore. And the code in the Program file is more concise and simple. TODO: Code Coverage is not ignoring this file when running GitHub Actions using dot net cli and SonarCloud.
  - `Middleware`: Implements the IMiddleware interface and is responsible for global error handling. It intercepts exceptions thrown by the application and throw the corret exception.
  - `ValidationFilterAttribute.cs`: Validates DTO required properties.
- `Business`: This project uses a generic Interface in all classes, in order to make it simple to make dependency injection. We only need to register one time in the Program.cs file and it is going to inject all business classes into the system commented. All classes receive an AutoMapper and a Log via dependency injection.
  - `IBusiness`: This folder contains the interfaces for the business logic components.
- `Entities`: This folder contains the entity models.
  - `DTO`: This folder contains the data transfer objects (DTOs) used for request and response payloads.
  - `Entity`: This folder contains the database entities. There is a many-to-many relationship example with an explicit class to handle it. There is also a BaseEntity class with a generic validate method for the entities.
  - `MapperProfile`: This file contains the AutoMapper mappings profile between DTOs and entity models.
- `Persistence`: This folder contains the data access layer components, using Entity Framework Core ORM.
  - `Context`: It has an ApiContext that inherits from DbContext, which is a class provided by Entity Framework Core that represents a session with the database and allows you to query and save instances of your entity classes.
  - `Migrations`: Contain all migration files defining the changes to the model that should be applied to the database. NOTE: We are using SQL Server for production and Development and SQLite to run Integration Tests. In order to run the same migrations for different providers, we need to edit the migration and create a conditional to verify if the provider is SQL Server or SQLite. The difference between them is the AutoIncrement syntax for Primary Keys.
  - `Repository`: This folder contains the repository classes that handle database operations.
  - `IRepository`: This folder contains the interfaces for the repository classes.
