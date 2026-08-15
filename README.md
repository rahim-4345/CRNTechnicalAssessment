# CRN Technical Assessment

## Overview

RESTful Backend API Solution developed using .NET 8 and C#.

## Tech Stack

- .NET 8
- ASP.NET Core Web API
- C#
- Entity Framework Core
- SQL Server
- JWT Authentication
- Refresh Token
- Swagger / OpenAPI
- xUnit
- Moq

## Architecture

The solution follows a layered architecture:

- API
- Application
- Domain
- Infrastructure
- API.Tests
- Application.Tests
- Infrastructure.Tests

## Features

- Product CRUD operations
- User authentication
- JWT access token
- Refresh token
- Entity Framework Core
- SQL Server database
- Swagger API documentation
- Unit and integration tests

## How to Run

1. Clone the repository.
2. Open `CRNTechnicalAssessment.sln` in Visual Studio 2022.
3. Update the SQL Server connection string in `appsettings.json`.
4. Apply database migrations.
5. Run the API project.
6. Open Swagger to test the APIs.

## Database

Database used:

`CRNTechnicalAssessmentDB`

## Testing

Tests are implemented using:

- xUnit
- Moq
- WebApplicationFactory
