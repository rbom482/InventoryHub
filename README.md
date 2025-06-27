# InventoryHub - Full Stack Integration Project

## Overview
InventoryHub is a full-stack application built with .NET backend and Blazor WebAssembly frontend, demonstrating integration patterns and API standardization.

## Architecture

### Backend (.NET 8)
- **API Controllers**: RESTful endpoints using ASP.NET Core
- **Minimal APIs**: Lightweight endpoints for simple operations
- **Entity Framework**: Data access layer with SQL Server
- **CORS**: Cross-origin resource sharing configuration

### Frontend (Blazor WebAssembly)
- **Component-based UI**: Blazor components with Razor syntax
- **HTTP Client**: API integration with error handling
- **Bootstrap**: Responsive UI framework

## API Standardization

### Standardized JSON Response Format

The `/api/productlist` endpoint returns data in a standardized format:

```json
{
  "success": true,
  "message": "Products retrieved successfully",
  "data": [
    {
      "id": 1,
      "name": "Laptop",
      "price": 1200.50,
      "stock": 25,
      "category": {
        "id": 101,
        "name": "Electronics",
        "description": "Electronic devices and gadgets"
      },
      "createdAt": "2024-12-27T10:00:00Z",
      "updatedAt": "2024-12-22T15:30:00Z",
      "isActive": true
    }
  ],
  "totalCount": 8,
  "timestamp": "2024-12-27T16:45:00Z"
}
```

### Key Features
- **Nested Category Object**: Each product includes complete category information
- **Response Wrapper**: Consistent API response structure with metadata
- **Decimal Precision**: Proper decimal handling for prices
- **Timestamps**: Creation and update tracking
- **Status Indicators**: Active/inactive product status

## Running the Application

### Backend
```bash
cd backend
dotnet run
```
Access Swagger UI at: `https://localhost:7001/swagger`

### Frontend
```bash
cd ClientApp
dotnet run
```
Access application at: `https://localhost:7000`

## Testing the API

### Using Browser
Navigate to: `https://localhost:7001/api/productlist`

### Using Postman
1. Create GET request to `https://localhost:7001/api/productlist`
2. Verify JSON structure includes:
   - Product details (id, name, price, stock)
   - Nested category object
   - Response metadata

### Using Debug Tool
Access the built-in debug tool at: `https://localhost:7000/debug`

## Project Structure

```
FullStackApp/
├── backend/
│   ├── Controllers/
│   │   ├── ProductsController.cs
│   │   └── TasksController.cs
│   ├── Models/
│   │   ├── ProductResponse.cs
│   │   └── TaskItem.cs
│   ├── Data/
│   │   └── ApplicationDbContext.cs
│   └── Program.cs
├── ClientApp/
│   ├── Pages/
│   │   ├── FetchProducts.razor
│   │   ├── Debug.razor
│   │   └── ...
│   ├── Layout/
│   │   └── NavMenu.razor
│   └── Program.cs
└── README.md
```

## Development Workflow

1. **Backend Development**: Create/modify API endpoints
2. **Model Standardization**: Ensure consistent JSON structures
3. **Frontend Integration**: Update Blazor components to consume APIs
4. **Testing**: Validate API responses and UI functionality
5. **Documentation**: Update README and API documentation

## Error Handling

The application includes comprehensive error handling:
- Network timeouts
- JSON parsing errors
- HTTP status errors
- CORS issues
- API response validation

## Future Enhancements

- Database integration with Entity Framework
- Authentication and authorization
- Advanced filtering and pagination
- Real-time updates with SignalR
- Deployment configuration