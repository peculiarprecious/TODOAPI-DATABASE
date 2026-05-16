# Enhanced TODO API with Database

A production quality RESTful API built with ASP.NET Core Web API and Entity Framework Core.
Implements a full database-backed architecture with Models, DTOs, Services, comprehensive validation,
standardized error handling, and proper HTTP status codes. Data is persisted using SQL Server LocalDB,
ensuring todos are stored in database for data persistency.


## Tech Stack

| Technology | Purpose |
|---|---|
| C# / ASP.NET Core Web API | Core framework |
| .NET 8 | Runtime |
| Entity Framework Core | Database ORM |
| SQL Server (LocalDB) | Database |
| Swagger / OpenAPI | API documentation & testing |

## Database

This project uses Entity Framework Core with SQL Server LocalDB.

## Database Setup & Migrations

### Connection String
The database connection is configured in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TodoApiDb;Trusted_Connection=True;"
  }
}
```

### Prerequisites
Make sure you have Entity Framework Core tools installed:
```cmd
dotnet tool install --global dotnet-ef
```

### How to Run Migrations

1. Create the migration (only needed once or when model changes)
```cmd
dotnet ef migrations add InitialCreate
```

2. Apply migration to create the database
```cmd
dotnet ef database update
```

3. Verify database was created in SSMS
   - Open SQL Server Management Studio
   - Connect to: `(localdb)\mssqllocaldb`
   - Look for `TodoApiDb` under Databases
   - Navigate to: `Tables → dbo.TodoItems`

### What Migrations Do
| Command | What it does |
|---|---|
| `migrations add InitialCreate` | Creates migration files from your Model |
| `database update` | Runs migration and creates database tables |

### If You Change the Model
When you add or modify properties in `TodoItem.cs`:
```cmd
dotnet ef migrations add YourMigrationName
dotnet ef database update
```

### Check Data in SSMS
1. Open SQL Server Management Studio
2. Connect to: (localdb)\mssqllocaldb
3. Navigate to: Databases → TodoApiDb → Tables → dbo.TodoItems
4. Right-click → Select Top 1000 Rows

### Verify Data in SSMS
After posting todos you can verify data was saved:
```sql
USE TodoApiDb;
SELECT * FROM TodoItems;
```

## Project Structure

TODOAPI-DATABASE/
├── Controllers/
│   └── TodoController.cs          # Handles HTTP requests
├── Data/
│   └── ApplicationDBContext.cs            # Entity Framework DB context
├── DTOs/
│   ├── CreateTodoDTO.cs           # Input DTO for creating todos
│   ├── UpdateTodoDTO.cs           # Input DTO for updating todos
│   └── TodoResponseDTO.cs         # Output DTO for responses
├── Migrations/                    # Auto-generated EF migrations
├── Models/
│   └── TodoItem.cs                # Core data model
├── Responses/
│   └── ErrorResponse.cs           # Standardized error format
├── Services/
│   ├── ITodoService.cs            # Service interface
│   └── TodoService.cs             # Business logic
├── appsettings.json               # App configuration & connection string
└── Program.cs                     # App entry point & configuration


## Getting Started

### Prerequisites
- .NET 8 SDK
- Visual Studio 2022
- SQL Server Management Studio (SSMS) 

### Installation

1. Clone the repository
   git clone https://github.com/peculiarprecious/TODOAPI-DATABASE.git

2. Navigate to project folder
   cd TODOAPI-DATABASE

3. Restore dependencies
   dotnet restore

4. Apply database migrations
   dotnet ef migrations add InitialCreate
   dotnet ef database update

5. Run the project
   dotnet run

6. Open Swagger UI
   https://localhost:7180/swagger


## API Endpoints

Base URL: https://localhost:7180/api/Todo

| Method | Endpoint | Description | Status Codes |
|--------|----------|-------------|--------------|
| GET | /api/Todo | Get all todos | 200 |
| GET | /api/Todo/{id} | Get todo by ID | 200, 404 |
| POST | /api/Todo | Create new todo | 201, 400 |
| PUT | /api/Todo/{id} | Update todo | 200, 400, 404 |
| DELETE | /api/Todo/{id} | Delete todo | 204, 404 |


## Request & Response Examples

### GET /api/Todo
Response 200 OK
[
  {
    "id": 1,
    "title": "Buy groceries",
    "description": "Milk and eggs",
    "isCompleted": false,
    "createdAt": "2026-05-08T10:30:00",
    "dueDate": "2026-12-01T00:00:00",
    "priority": "High"
  }
]


### POST /api/Todo
Request Body
{
  "title": "Buy groceries",
  "description": "Milk and eggs",
  "dueDate": "2026-12-01T00:00:00",
  "priority": "High"
}

Response 201 Created
{
  "id": 1,
  "title": "Buy groceries",
  "description": "Milk and eggs",
  "isCompleted": false,
  "createdAt": "2026-05-08T10:30:00",
  "dueDate": "2026-12-01T00:00:00",
  "priority": "High"
}

Response 400 Bad Request
{
  "statusCode": 400,
  "message": "Validation failed",
  "errors": {
    "Title": ["Title is required"]
  },
  "timestamp": "2026-05-08T10:30:00"
}


### PUT /api/Todo/{id}
Request Body
{
  "title": "Updated title",
  "description": "Updated description",
  "isCompleted": true,
  "dueDate": "2026-12-01T00:00:00",
  "priority": "Medium"
}

Response 200 OK — returns updated todo
Response 404 Not Found — id does not exist
Response 400 Bad Request — validation failed


### DELETE /api/Todo/{id}
Response 204 No Content — successfully deleted
Response 404 Not Found — id does not exist

## Validation Rules

| Field | Rules |
|-------|-------|
| Title | Required, 3-100 characters, no whitespace only |
| Description | Optional, max 500 characters |
| Priority | Must be Low, Medium, or High |
| DueDate | Optional, cannot be in the past |


## Error Response Format

All errors return this standardized format:
{
  "statusCode": 400,
  "message": "Validation failed",
  "errors": {
    "FieldName": ["Error message"]
  },
  "timestamp": "2026-05-08T10:30:00"
}


## Status Codes

| Code | Meaning |
|------|---------|
| 200 OK | Request successful |
| 201 Created | Resource created successfully |
| 204 No Content | Deleted successfully |
| 400 Bad Request | Validation failed |
| 404 Not Found | Resource not found |
| 500 Internal Server Error | Server error |


## Author

Precious Nwajei
GitHub: https://github.com/peculiarprecious

![alt text](<Screenshot (763)-1.png>)