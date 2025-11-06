# Speakers API

An ASP.NET Core Web API for managing speaker information with full CRUD operations, built with .NET 9, Entity Framework Core, and Azure monitoring integration.

https://github.com/user-attachments/assets/6d741206-d199-4a18-b5e5-3ad53841352e

## Features

- **RESTful API** for speaker management (CRUD operations)
- **Entity Framework Core** with SQL Server database
- **Repository Pattern** with generic implementation
- **OpenTelemetry Integration** for distributed tracing and logging
- **Azure Application Insights** monitoring
- **Auto-seeding** with Bogus library for development data
- **OpenAPI** documentation with Scalar UI

## Tech Stack

- **.NET 9.0**
- **ASP.NET Core Web API**
- **Entity Framework Core 9.0**
- **SQL Server** (LocalDB for development)
- **OpenTelemetry** for observability
- **Azure Monitor** for application insights
- **Scalar** for API documentation

## Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- SQL Server or SQL Server LocalDB
- Visual Studio 2022 / VS Code / Rider

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/vizsphere/Speakers_Api.git
cd speakers-api
```

### 2. Update Configuration

Update `appsettings.json` with your database connection string and Application Insights connection string:

```json
{
  "AppSettings": {
    "ConnectionString": "Server=(localdb)\\MSSQLLocalDB;Database=SpeakersDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True",
    "ApplicationInsightConnectionString": "your-application-insights-connection-string"
  }
}
```

### 3. Restore Packages

```bash
dotnet restore
```

### 4. Run the Application

```bash
dotnet run
```

The API will be available at:
- HTTPS: `https://localhost:7070`
- HTTP: `http://localhost:5106`

## API Endpoints

### Get All Speakers
```http
GET /Speakers/getSpeakers
```

### Get Speaker by ID
```http
GET /Speakers/getSpeakerById?id={speakerId}
```

### Search Speakers
```http
POST /Speakers/searchSpeaker?searchTerm={term}
```

### Add Speaker
```http
POST /Speakers/addSpeaker
Content-Type: application/json

{
  "name": "John Doe",
  "bio": "Experienced speaker in cloud technologies",
  "webSite": "https://johndoe.com"
}
```

### Update Speaker
```http
POST /Speakers/updateSpeaker
Content-Type: application/json

{
  "id": "speaker-id",
  "name": "John Doe",
  "bio": "Updated bio",
  "webSite": "https://johndoe.com"
}
```

### Delete Speaker
```http
POST /Speakers/deleteSpeaker?id={speakerId}
```

## Project Structure

```
├── Controllers/
│   └── SpeakersController.cs    # API endpoints
├── Models/
│   ├── AppSettings.cs            # Configuration model
│   └── SpeakerDbContext.cs       # EF Core DbContext
├── Repositories/
│   ├── IGenericRepository.cs     # Generic repository interface & implementation
│   └── ISpeakerRepository.cs     # Speaker-specific repository
├── Services/
│   └── SpeakerService.cs         # Business logic layer
├── Domain/
│   └── Speaker.cs                # Speaker entity (referenced)
└── Program.cs                    # Application startup & configuration
```


### Key Design Patterns

- **Repository Pattern** - Abstracts data access logic
- **Dependency Injection** - Used throughout for loose coupling
- **Generic Repository** - Reusable data access pattern

## Database

The application uses Entity Framework Core with SQL Server. In development mode, the database is automatically seeded with 100 fake speakers using the Bogus library.


## Observability

### OpenTelemetry Integration

The application includes comprehensive observability through OpenTelemetry:

- **Distributed Tracing** - ASP.NET Core and HTTP client instrumentation
- **Structured Logging** - OpenTelemetry logging with Azure Monitor export
- **Azure Application Insights** - Full telemetry export


### Application Insights Queries
```
traces
| where  message has "Getting all speakers"
| order by timestamp, operation_Name desc

traces
| where  message has "Adding new speaker"
| order by timestamp, operation_Name desc


traces
| where  message has "Getting speaker by ID"
| order by timestamp, operation_Name desc


exceptions
| where  problemId has "EntityFrameworkCore"
| order by timestamp, operation_Name desc

```
