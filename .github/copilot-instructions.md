# Copilot Instructions

# Repository Coding Guidelines

This project is built using ASP.NET Core Web API and follows Clean Architecture principles.

## Coding Rules
- Use specific formatting rules
- Follow naming conventions
- Use C# async/await for all I/O operations.
- Prefer code modernized upto C# 14 and .NET 10 if possible else older ones are fine.
- Use dependency injection for services and repositories.
- Follow Repository pattern for database access.
- Controllers should be thin and call services.
- Services contain business logic.
- Use DTOs instead of returning database entities.

## Naming Conventions

- Services end with Service
- Repositories end with Repository
- Interfaces start with I

Example:
UserService
IUserService
UserRepository

## Error Handling

Use middleware-based global exception handling.

## Testing

Unit tests should use xUnit and Moq.

## Performance

Avoid blocking calls and always use async database queries.