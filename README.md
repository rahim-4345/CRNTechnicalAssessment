<<<<<<< HEAD
# CRN Technical Assessment - RESTful Backend API

## Overview

This project is a RESTful Backend API developed using .NET 8, ASP.NET Core Web API, Entity Framework Core, and SQL Server.

The application provides APIs for user authentication and Product CRUD operations.
=======
# CRN Technical Assessment

## Overview

RESTful Backend API Solution developed using .NET 8 and C#.
>>>>>>> d015fe1e6e352292cc7f9cd0b87fb987c39b29d4

## Tech Stack

- .NET 8
<<<<<<< HEAD
- C#
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger / OpenAPI
- Docker
- Docker Compose
=======
- ASP.NET Core Web API
- C#
- Entity Framework Core
- SQL Server
- JWT Authentication
- Refresh Token
- Swagger / OpenAPI
>>>>>>> d015fe1e6e352292cc7f9cd0b87fb987c39b29d4
- xUnit
- Moq

## Architecture

The solution follows a layered architecture:

- API
- Application
- Domain
- Infrastructure
<<<<<<< HEAD

## Features

- User Registration
- User Login
- JWT Authentication
- Product CRUD
- Entity Framework Core
- SQL Server database
- Global error handling
- Input validation
- Swagger API documentation
- Docker support
- Docker Compose support

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
| POST | `/api/Products` | Create product |
| PUT | `/api/Products/{id}` | Update product |
| DELETE | `/api/Products/{id}` | Delete product |

## Running Locally

### Prerequisites

- .NET 8 SDK
- SQL Server
- Visual Studio 2022
- Docker Desktop

### Database

Create the required SQL Server database and configure the connection string in `appsettings.json`.

### Run Application

```bash
dotnet restore
dotnet build
dotnet run

# CRN Technical Assessment

## Project Overview

## Technologies Used

## Features

## API Endpoints

## Authentication

## Database

## Docker Setup

## Running the Application

## Running Tests

## Test Results

## Running the Application

1. Clone the repository.
2. Open the solution in Visual Studio.
3. Start Docker Desktop.
4. Run:

docker compose up --build

5. Open Swagger.

## Running Tests

dotnet test
=======
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
>>>>>>> d015fe1e6e352292cc7f9cd0b87fb987c39b29d4

# CRN Technical Assessment – RESTful Backend API

## Overview

This project is a RESTful Backend API developed using .NET 8,
ASP.NET Core Web API, Entity Framework Core, and SQL Server.