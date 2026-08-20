# CRN Technical Assessment – RESTful Backend API

## Overview

This project is a RESTful Backend API developed as part of the CRN Technical Assessment using .NET 8 and C#.

The application provides user authentication and Product CRUD operations using ASP.NET Core Web API, Entity Framework Core, and SQL Server.

## Tech Stack

- .NET 8
- C#
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- Refresh Token
- Swagger / OpenAPI
- xUnit
- Moq
- WebApplicationFactory

## Architecture

The solution follows a layered architecture:

- API
- Application
- Domain
- Infrastructure

This architecture helps maintain separation of concerns, scalability, maintainability, and testability.

## Features

### Authentication

- User Registration
- User Login
- JWT Access Token
- Refresh Token
- Authentication and Authorization

### Product Management

- Create Product
- Get All Products
- Get Product by ID
- Update Product
- Delete Product

### Other Features

- Entity Framework Core
- SQL Server Database
- Input Validation
- Global Error Handling
- Swagger API Documentation
- Unit Testing
- Integration Testing

## API Endpoints

### Authentication

| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/Auth/register` | Register a new user |
| POST | `/api/Auth/login` | Login and generate JWT token |

### Products

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/Products` | Get all products |
| GET | `/api/Products/{id}` | Get product by ID |
| POST | `/api/Products` | Create a new product |
| PUT | `/api/Products/{id}` | Update an existing product |
| DELETE | `/api/Products/{id}` | Delete a product |

## Prerequisites

Before running the application, make sure the following are installed:

- .NET 8 SDK
- Visual Studio 2022
- SQL Server
- SQL Server Management Studio

## Database Configuration

The application uses SQL Server with Entity Framework Core.

Database name:

`CRNTechnicalAssessmentDB`

Update the SQL Server connection string in:

`appsettings.json`

### Example Configuration

> The credentials below are placeholders. Do not commit real passwords, JWT secrets, or other sensitive credentials to GitHub.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=CRNTechnicalAssessmentDB;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "YOUR_JWT_SECRET_KEY",
    "Issuer": "CRNTechnicalAssessment",
    "Audience": "CRNTechnicalAssessmentUsers",
    "ExpiryMinutes": 60,
    "AccessTokenMinutes": 15,
    "RefreshTokenDays": 7
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}