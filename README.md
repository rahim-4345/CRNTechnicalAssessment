# CRN Technical Assessment - RESTful Backend API

## Overview

This project is a RESTful Backend API developed using .NET 8, ASP.NET Core Web API, Entity Framework Core, and SQL Server.

The application provides APIs for user authentication and Product CRUD operations.

## Tech Stack

- .NET 8
- C#
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger / OpenAPI
- Docker
- Docker Compose
- xUnit
- Moq

## Architecture

The solution follows a layered architecture:

- API
- Application
- Domain
- Infrastructure

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