# ProjectTracker.API

A task and project management REST API built with .NET 8, ASP.NET Core Web  API, and Entity Framework Core. It provides secure JWT authentication, role-based authorization, and fully containerized integration testing with PostgreSQL Testcontainers.

## Features

### Core Functionality

- **Project Management** - Full CRUD Support with optimized data retrieval and DTO projections
- **User Authentication** - Secure JWT-based login and token handling
- **Identity Management** - ASP.NET Identity with strong password policies and email uniqueness
- **Role-Based Access Control** - Authorization using roles and bearer tokens

### API Endpoints

#### Authentication (`/api/auth`)

- `POST /register` - Create a new user account
- `POST /login` - Authenticate and retrieve JWT

#### Projects (`/api/projects`)

- `GET /api/projects` - Fetch all project summaries
- `GET /api/projects/{id}` - Retrieve project details by ID
- `POST /api/projects` - Create a new project
- `PUT /api/projects/{id}` - Update an existing project
- `DELETE /api/projects/{id}` - Delete a project

#### Tasks (`/api/tasks`)

- `GET /api/tasks` - Retrieve all task summaries
- `GET /api/tasks{id}` - Get task details by ID
- `POST /api/tasks` - Create a new task
- `PUT /api/tasks/{id}` - Update task details
- `GET /api/tasks/{id}` - Delete a task

### Technical Highlights

- **Swagger Integration** - Interactive API documentation with built-in JWT authentication support
- **Clean Architeture** - Clear separation of concerns across layers (DTOs, Services, Mappings)
- **Entity Framework Core** - Code-first database with migrations
- **Strong Password Security** - ASP.NET Identity enforcing robust password policies
- **Performance Focused** - Lightweight summary DTOs for list endpoints and detailed DTOs for specific requests
- **Integration Testing** - PostgreSQL Testcontainers for isolated, repeatable tests

## Architecture

### Design Patterns

- **Repository Pattern** - Abstracted data operations via EF Core's DbContext
- **DTO Pattern** - Separate read/write models to optimize data flow
- **Mapper Pattern** - Extension methods for entity-to-DTO mapping
- **Dependency Injection** - Managed service dependencies via ASP.NET Core DI
- **Factory Pattern** - Used for integration test setup through `TestWebApplicationFactory`

## Tech Stack

### Backend Framework

- **.NET 8.0 (LTS)** - Latest runtime and C# 12 features
- **ASP.NET Core Web API 8.0.21**
- **Entity Framework Core 8.0.21**

### Authentication & Security

- **ASP.NET Identity** - User registration and role managemene
- **JWT Bearer Authentication** - Secure access control

### Database

- **SQL Server** - Primary production database
- **PostgreSQL Testcontainer** - Used in automated integration tests
- **EF Migrations** - Database schema version control

### Testing

- **xUnit** - Unit and integration testing
- **Testcontainers** - Containerized PostgreSQL for clean test environments
- **Microsoft.AspNetCore.Mvc.Testing** - Api testing utilities

### Documentation

- **Swagger (Swashbuckle.AspNetCore)** - Interactive API documentation
- **OpenAPI 3.0** - Specification compliance

### Development Tools

- **VS Code** - with C# extensions
- **SQL Server Management Studio**
- **Docker** - Required for containerized testing
- **.NET CLI** - Build and run utilities

## Getting Started (TODO)
