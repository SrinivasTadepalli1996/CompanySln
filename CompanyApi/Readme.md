Company API
Overview
The Company API is a .NET 7 Web API designed to manage company-related data. It uses Entity Framework Core with an in-memory database, follows clean architecture principles, and includes global exception handling for robustness. The API is containerized using Docker and provides Swagger UI for API exploration.

Features
CRUD Operations for managing companies.
In-Memory Database for lightweight data storage.
Global Exception Handling Middleware for improved error handling.
CORS Configuration to allow cross-origin requests.
Swagger UI Integration for API testing.
Docker Support for containerized deployment.

CompanyApi/
│-- CompanyApi.Data/            # Data Layer - EF Core DbContext
│-- CompanyApi.Repositories/    # Data Repositories
│-- CompanyApi.Services/        # Business Logic Layer
│-- CompanyApi.Interfaces/      # Interfaces for Dependency Injection
│-- CompanyApi.Middleware/      # Custom Middleware for Error Handling
│-- appsettings.json            # Configuration Settings
│-- Program.cs                  # Application Startup & Configuration
│-- Dockerfile                  # Docker Image Configuration
│-- README.md                   # Project Documentation

Setup & Installation
Prerequisites
.NET SDK 7.0+
Docker & Docker Compose
Git

git clone https://github.com/SrinivasTadepalli199454/CompanySln.git
cd CompanySln-main

docker-compose up --build

Can be run locally also 

Api documentation:
http://localhost:5168/swagger
