# Person Web API - Project Summary

## Project Complete

A fully functional .NET Web API with file persistence has been successfully created.

## Project Location
```
/Users/kevinorange/Downloads/Software Engineering/C#WebApi/C--WebApi/PersonWebApi/
```

## Requirements Completed

### 1. Person Model Class
**File:** `Models/Person.cs`
- `id`: string (auto-generated GUID)
- `Name`: string
- `School`: string

### 2. Strongly Typed Web API
**File:** `Controllers/PersonController.cs`
- Full CRUD operations (GET, POST, PUT, DELETE)
- Object mapping for Person
- Proper HTTP response codes
- Input validation

### 3. FileManager Singleton
**File:** `Services/FileManager.cs`
- Thread-safe singleton implementation
- JSON file persistence using Newtonsoft.Json
- File locking for concurrent access
- Auto-initialization of data file

### 4. JSON Persistence
**File:** `persons.json` (auto-created in project root)
- Uses Newtonsoft.Json for serialization
- Formatted JSON with indentation
- Sample data already created

### 5. Swagger Documentation
- Accessible at: `http://localhost:5000`
- Full API documentation
- Interactive testing interface

## How to Run

```bash
cd "/Users/kevinorange/Downloads/Software Engineering/C#WebApi/C--WebApi/PersonWebApi"
dotnet run
```

**Server will start at:** `http://localhost:5000`
**Swagger UI:** `http://localhost:5000` (open in browser)

## Screenshots Required

### 1. Swagger Page Screenshot
- **URL:** `http://localhost:5000`
- Shows all API endpoints with Swagger UI

### 2. JSON File Screenshot
- **File Location:** `PersonWebApi/persons.json`
- Contains sample records:
  - John Doe (MIT)
  - Jane Smith (Stanford University)

### 3. FileManager Code Screenshot
- **File:** `PersonWebApi/Services/FileManager.cs`
- Shows thread-safe singleton implementation
- Demonstrates Newtonsoft.Json usage

## Sample Data Already Created

The following records are already in the `persons.json` file:

```json
[
  {
    "Id": "c08c9705-ce7a-420d-b8b3-43c170ec1f08",
    "Name": "John Doe",
    "School": "MIT"
  },
  {
    "Id": "52a1459c-43c9-4316-8cac-00072ab89dc1",
    "Name": "Jane Smith",
    "School": "Stanford University"
  }
]
```

## Testing the API

### Get All Persons
```bash
curl http://localhost:5000/api/Person
```

### Create New Person
```bash
curl -X POST http://localhost:5000/api/Person \
  -H "Content-Type: application/json" \
  -d '{"name":"Alice Johnson","school":"UC Berkeley"}'
```

### Get Person by ID
```bash
curl http://localhost:5000/api/Person/c08c9705-ce7a-420d-b8b3-43c170ec1f08
```

### Update Person
```bash
curl -X PUT http://localhost:5000/api/Person/c08c9705-ce7a-420d-b8b3-43c170ec1f08 \
  -H "Content-Type: application/json" \
  -d '{"name":"John Doe Updated","school":"Harvard"}'
```

### Delete Person
```bash
curl -X DELETE http://localhost:5000/api/Person/c08c9705-ce7a-420d-b8b3-43c170ec1f08
```

## Dependencies

- **Microsoft.AspNetCore.Mvc.NewtonsoftJson** (10.0.3)
- **Newtonsoft.Json** (13.0.4)
- **Swashbuckle.AspNetCore** (10.1.3)

## Architecture Highlights

### Thread-Safe Singleton Pattern
```csharp
public sealed class FileManager
{
    private static readonly object _lock = new object();
    private static FileManager? _instance;
    
    public static FileManager Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new FileManager();
                    }
                }
            }
            return _instance;
        }
    }
}
```

### File Locking
```csharp
private readonly object _fileLock = new object();

public List<Person> GetAllPersons()
{
    lock (_fileLock)
    {
        string json = File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<List<Person>>(json) 
            ?? new List<Person>();
    }
}
```

### Newtonsoft.Json Usage
```csharp
// Serialization with formatting
string json = JsonConvert.SerializeObject(persons, Formatting.Indented);

// Deserialization
List<Person> persons = JsonConvert.DeserializeObject<List<Person>>(json);
```

## Key Files for Review

1. **Person Model:** [Models/Person.cs](Models/Person.cs)
2. **FileManager:** [Services/FileManager.cs](Services/FileManager.cs)
3. **Person Controller:** [Controllers/PersonController.cs](Controllers/PersonController.cs)
4. **Program Configuration:** [Program.cs](Program.cs)
5. **Data File:** [persons.json](persons.json)

## Additional Features Implemented

- Auto-generation of IDs (GUID)
- Input validation (Name and School required)
- Proper error handling
- RESTful API design
- HTTP status codes (200, 201, 404, 400)
- Swagger/OpenAPI documentation
- Development environment configuration

---

**Project Status:** COMPLETE AND READY FOR SUBMISSION
