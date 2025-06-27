using Microsoft.EntityFrameworkCore;
using FullStackApp.Backend.Data;
using FullStackApp.Backend.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add CORS configuration as specified
app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

app.UseAuthorization();

// Add Minimal API endpoint for standardized JSON format
app.MapGet("/api/productlist", () =>
{
    var products = new List<ProductResponse>
    {
        new ProductResponse
        {
            Id = 1,
            Name = "Laptop",
            Price = 1200.50m,
            Stock = 25,
            Category = new CategoryResponse { Id = 101, Name = "Electronics", Description = "Electronic devices and gadgets" },
            CreatedAt = DateTime.UtcNow.AddDays(-30),
            UpdatedAt = DateTime.UtcNow.AddDays(-5),
            IsActive = true
        },
        new ProductResponse
        {
            Id = 2,
            Name = "Headphones",
            Price = 50.00m,
            Stock = 100,
            Category = new CategoryResponse { Id = 102, Name = "Accessories", Description = "Computer and electronic accessories" },
            CreatedAt = DateTime.UtcNow.AddDays(-25),
            UpdatedAt = DateTime.UtcNow.AddDays(-3),
            IsActive = true
        },
        new ProductResponse
        {
            Id = 3,
            Name = "Wireless Mouse",
            Price = 35.99m,
            Stock = 75,
            Category = new CategoryResponse { Id = 102, Name = "Accessories", Description = "Computer and electronic accessories" },
            CreatedAt = DateTime.UtcNow.AddDays(-20),
            UpdatedAt = DateTime.UtcNow.AddDays(-2),
            IsActive = true
        },
        new ProductResponse
        {
            Id = 4,
            Name = "4K Monitor",
            Price = 299.99m,
            Stock = 15,
            Category = new CategoryResponse { Id = 101, Name = "Electronics", Description = "Electronic devices and gadgets" },
            CreatedAt = DateTime.UtcNow.AddDays(-15),
            UpdatedAt = DateTime.UtcNow.AddDays(-1),
            IsActive = true
        },
        new ProductResponse
        {
            Id = 5,
            Name = "Mechanical Keyboard",
            Price = 89.99m,
            Stock = 45,
            Category = new CategoryResponse { Id = 102, Name = "Accessories", Description = "Computer and electronic accessories" },
            CreatedAt = DateTime.UtcNow.AddDays(-12),
            IsActive = true
        },
        new ProductResponse
        {
            Id = 6,
            Name = "Webcam HD",
            Price = 79.99m,
            Stock = 30,
            Category = new CategoryResponse { Id = 101, Name = "Electronics", Description = "Electronic devices and gadgets" },
            CreatedAt = DateTime.UtcNow.AddDays(-10),
            IsActive = true
        },
        new ProductResponse
        {
            Id = 7,
            Name = "USB-C Hub",
            Price = 49.99m,
            Stock = 60,
            Category = new CategoryResponse { Id = 102, Name = "Accessories", Description = "Computer and electronic accessories" },
            CreatedAt = DateTime.UtcNow.AddDays(-8),
            IsActive = true
        },
        new ProductResponse
        {
            Id = 8,
            Name = "External SSD",
            Price = 149.99m,
            Stock = 20,
            Category = new CategoryResponse { Id = 103, Name = "Storage", Description = "Data storage solutions" },
            CreatedAt = DateTime.UtcNow.AddDays(-5),
            IsActive = true
        }
    };

    var response = new ApiResponse<List<ProductResponse>>
    {
        Success = true,
        Message = "Products retrieved successfully",
        Data = products,
        TotalCount = products.Count,
        Timestamp = DateTime.UtcNow
    };

    return Results.Ok(response);
})
.WithName("GetProductList")
.WithTags("Products")
.WithOpenApi();

// Add validation endpoint for JSON structure testing
app.MapGet("/api/productlist/validate", () =>
{
    var sampleProduct = new ProductResponse
    {
        Id = 1,
        Name = "Sample Product",
        Price = 99.99m,
        Stock = 10,
        Category = new CategoryResponse 
        { 
            Id = 101, 
            Name = "Sample Category",
            Description = "This is a sample category for validation"
        },
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow,
        IsActive = true
    };

    var validationResponse = new
    {
        Success = true,
        Message = "JSON structure validation endpoint",
        SampleProduct = sampleProduct,
        RequiredFields = new[]
        {
            "id (integer)",
            "name (string)",
            "price (decimal)",
            "stock (integer)",
            "category (object)",
            "category.id (integer)",
            "category.name (string)",
            "category.description (string, optional)",
            "createdAt (datetime)",
            "updatedAt (datetime, optional)",
            "isActive (boolean)"
        },
        Timestamp = DateTime.UtcNow
    };

    return Results.Ok(validationResponse);
})
.WithName("ValidateProductStructure")
.WithTags("Products")
.WithOpenApi();

app.MapControllers();

app.Run();
