# Person Web API - C# .NET Project

A RESTful Web API built with .NET 10.0 for managing Person data with JSON file persistence.

## Project Structure

```
C--WebApi/
└── PersonWebApi/          # Main Web API project
    ├── Models/
    │   └── Person.cs      # Person model (Id, Name, School)
    ├── Services/
    │   └── FileManager.cs # Thread-safe singleton for JSON persistence
    ├── Controllers/
    │   └── PersonController.cs # REST API endpoints
    ├── Program.cs         # Application configuration
    └── persons.json       # JSON data file (auto-created)
```

## How to Run

### From the workspace root:
```bash
cd C--WebApi/PersonWebApi
dotnet run
```

**OR**

```bash
cd C--WebApi
dotnet run --project PersonWebApi
```

### From the C--WebApi directory:
```bash
dotnet run --project PersonWebApi
```

The server will start at: **http://localhost:5048**

## Access the Application

- **Swagger UI:** http://localhost:5048
- **API Endpoints:** http://localhost:5048/api/Person

## Quick Test

### Using cURL:
```bash
# Create a new person
curl -X POST http://localhost:5048/api/Person \
  -H "Content-Type: application/json" \
  -d '{"name":"John Doe","school":"MIT"}'

# Get all persons
curl http://localhost:5048/api/Person
```

### Using Swagger UI:
1. Navigate to http://localhost:5048
2. Click on any endpoint to expand it
3. Click "Try it out"
4. Enter the required data
5. Click "Execute"

## Technologies

- .NET 10.0
- ASP.NET Core Web API
- Newtonsoft.Json (JSON serialization)
- Swashbuckle.AspNetCore (Swagger)


