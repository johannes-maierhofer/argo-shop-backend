# Argo Shop

> **The main idea for creating this project is to showcase a simple application in Clean Architecture style. It is heavily indfluenced by the [`Jason Taylor Clean Architecture Template`](https://github.com/jasontaylordev/CleanArchitecture).**

## The Goals of This Project

- Showcasing `Clean Architecture`.
- Basic `Domain Driven Design (DDD)` tactical patterns.
- Use of `Feature Folders` to implement use cases.
- Use of `Result Object Pattern` to avoid throwing errors.
- Handle cross-cutting concerns via `MediatR pipeline behaviors` (e.g. logging, validation, error-handling, authorization).
- Handle cross-cutting concerns via `EF Core interceptors` (raising domain events, handle auditable entities).
- `Integration Tests` - fully test all the features.

## Technologies - Libraries

- ✔️ **[`.NET 7`](https://dotnet.microsoft.com/download)** - .NET Core, ASP.NET Core
- ✔️ **[`EF Core`](https://github.com/dotnet/efcore)** - Modern object-database mapper for .NET. 
- ✔️ **[`MediatR`](https://github.com/jbogard/MediatR)** - Simple, unambitious mediator implementation in .NET.
- ✔️ **[`FluentValidation`](https://github.com/FluentValidation/FluentValidation)** - Popular .NET validation library for building strongly-typed validation rules
- ✔️ **[`Swagger & Swagger UI`](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)** - Swagger tools for documenting API's built on ASP.NET Core
- ✔️ **[`Serilog`](https://github.com/serilog/serilog)** - Simple .NET logging with fully-structured events
- ✔️ **[`AutoMapper`](https://docs.automapper.org/en/stable/)** - Convention-based object-object mapper in .NET.
- ✔️ **[`Respawn`](https://github.com/jbogard/Respawn)** - Respawn is a small utility to help in resetting test databases to a clean state.
- ✔️ **[`xUnit`](https://xunit.net/)** - free open source, community-focused unit testing tool for the .NET Framework.
- ✔️ **[`Microsoft.AspNetCore.Mvc.Testing`](https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-7.0)** - Facilitates integration tests with the help of the WebApplicationFactory class.
 

## Project References & Credits

- [Clean Architecture Template - by Jason Taylor](https://github.com/jasontaylordev/CleanArchitecture)
- [Clean Architecture with ASP.NET Core 3.0 (GOTO 2019) - Jason Taylor](https://www.youtube.com/watch?v=dK4Yb6-LxAk)
- [The Clean Architecture - by Robert C. Martin (Uncle Bob)](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
