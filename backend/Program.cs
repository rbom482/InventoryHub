using Microsoft.EntityFrameworkCore;
using FullStackApp.Backend.Data;
using FullStackApp.Backend.Models;
using FullStackApp.Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add response caching for performance optimization
builder.Services.AddResponseCaching();
builder.Services.AddMemoryCache();

// Add Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services for dependency injection
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add response caching middleware for performance
app.UseResponseCaching();

// Add CORS configuration as specified
app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

app.UseAuthorization();

// Add Minimal API endpoint for standardized JSON format with caching
app.MapGet("/api/productlist", async (IProductService productService) =>
{
    var products = await productService.GetProductsAsync();

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

// Add cache management endpoint for development
app.MapPost("/api/productlist/clear-cache", (IProductService productService) =>
{
    productService.ClearCache();
    return Results.Ok(new { Success = true, Message = "Cache cleared successfully", Timestamp = DateTime.UtcNow });
})
.WithName("ClearProductCache")
.WithTags("Products")
.WithOpenApi();

// Add individual product endpoint
app.MapGet("/api/productlist/{id}", async (int id, IProductService productService) =>
{
    var product = await productService.GetProductAsync(id);
    if (product == null)
    {
        return Results.NotFound(new ApiResponse<object>
        {
            Success = false,
            Message = $"Product with ID {id} not found",
            Data = null,
            Timestamp = DateTime.UtcNow
        });
    }

    var response = new ApiResponse<ProductResponse>
    {
        Success = true,
        Message = "Product retrieved successfully",
        Data = product,
        TotalCount = 1,
        Timestamp = DateTime.UtcNow
    };

    return Results.Ok(response);
})
.WithName("GetProduct")
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
