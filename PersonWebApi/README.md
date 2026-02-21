# Person Web API

A simple REST API for managing person data. Built with .NET 10.0 and stores data in a JSON file.

## What It Does

This API lets you create, read, update, and delete person records. Each person has:
- **Name** - The person's name
- **School** - The school they attend
- **Id** - Auto-generated unique identifier

## How to Build and Run

### Prerequisites
- .NET 10.0 SDK installed

### Step 1: Navigate to the Project Directory
```bash
cd C--WebApi
```

**Important:** You must be in the `C--WebApi` directory (where the `.sln` file is located) to build and run.

### Step 2: Build the Project
```bash
dotnet build
```

### Step 3: Run the Application
```bash
dotnet run --project PersonWebApi
```

The API will start at `http://localhost:5048`

### Step 4: View the API Documentation
Open your browser and go to:
```
http://localhost:5048
```

You'll see the Swagger UI where you can test all the endpoints.

## Quick Example

Once running, try creating a person:

```bash
curl -X POST http://localhost:5048/api/Person \
  -H "Content-Type: application/json" \
  -d '{"name":"John Doe","school":"MIT"}'
```

Then view all persons:
```bash
curl http://localhost:5048/api/Person
```

## API Endpoints

- `GET /api/Person` - Get all persons
- `GET /api/Person/{id}` - Get a specific person
- `POST /api/Person` - Create a new person
- `PUT /api/Person/{id}` - Update a person
- `DELETE /api/Person/{id}` - Delete a person

## Data Storage

Data is stored in `persons.json` in the project directory and is automatically created on first run.
