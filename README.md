# Payment Processing API

This is a sample Payment Processing API built with .NET 6. It provides endpoints for authorizing, capturing, and voiding payments, as well as retrieving transaction details.

## Getting Started

To get started with the Payment Processing API, follow these steps:

### Prerequisites

- .NET 6 (or higher)
- Visual Studio or Visual Studio Code (optional)

### Installation

1. Clone the repository:
2. Navigate to the project directory
3. Build the project

   dotnet build

4. run the api

   dotnet run

## API Endpoints

The Payment Processing API exposes the following endpoints:

- `POST /api/authorize`: Authorize a payment.
- `POST /api/authorize/{id}/capture`: Capture an authorized payment.
- `POST /api/authorize/{id}/voids`: Void an authorized payment.
- `GET /api/transactions`: Get all transactions with pagination support.

Refer to the API documentation for more details on the request and response formats of each endpoint.

## Architecture

The Payment Processing API follows the vertical slice architecture pattern. This pattern organizes the codebase based on features or capabilities instead of traditional layered architecture.

## MediatR

The MediatR library is used in this project to implement the Mediator pattern. It decouples the sender and receiver of a request by using a mediator object, allowing for easier maintenance and scalability. The MediatR library simplifies the implementation of command and query patterns.

## Configuration

The API configuration can be modified in the `appsettings.json` file. You can specify the database connection string, logging settings, and other configuration options. I used the sqlite with dapper.
