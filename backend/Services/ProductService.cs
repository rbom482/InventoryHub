using FullStackApp.Backend.Models;
using Microsoft.Extensions.Caching.Memory;

namespace FullStackApp.Backend.Services
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetProductsAsync();
        Task<ProductResponse?> GetProductAsync(int id);
        void ClearCache();
    }

    public class ProductService : IProductService
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<ProductService> _logger;
        private const string PRODUCTS_CACHE_KEY = "products_list";
        private const int CACHE_EXPIRATION_MINUTES = 15;

        public ProductService(IMemoryCache cache, ILogger<ProductService> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public async Task<List<ProductResponse>> GetProductsAsync()
        {
            if (_cache.TryGetValue(PRODUCTS_CACHE_KEY, out List<ProductResponse>? cachedProducts))
            {
                _logger.LogInformation("Products retrieved from cache");
                return cachedProducts!;
            }

            _logger.LogInformation("Products not in cache, generating new data");
            var products = await GenerateProductsAsync();
            
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CACHE_EXPIRATION_MINUTES),
                SlidingExpiration = TimeSpan.FromMinutes(5),
                Priority = CacheItemPriority.Normal
            };

            _cache.Set(PRODUCTS_CACHE_KEY, products, cacheOptions);
            _logger.LogInformation($"Products cached for {CACHE_EXPIRATION_MINUTES} minutes");
            
            return products;
        }

        public async Task<ProductResponse?> GetProductAsync(int id)
        {
            var products = await GetProductsAsync();
            return products.FirstOrDefault(p => p.Id == id);
        }

        public void ClearCache()
        {
            _cache.Remove(PRODUCTS_CACHE_KEY);
            _logger.LogInformation("Product cache cleared");
        }

        private async Task<List<ProductResponse>> GenerateProductsAsync()
        {
            // Simulate some async work (database call, etc.)
            await Task.Delay(10);

            var categories = new List<CategoryResponse>
            {
                new CategoryResponse { Id = 101, Name = "Electronics", Description = "Electronic devices and gadgets" },
                new CategoryResponse { Id = 102, Name = "Accessories", Description = "Computer and electronic accessories" },
                new CategoryResponse { Id = 103, Name = "Storage", Description = "Data storage solutions" }
            };

            return new List<ProductResponse>
            {
                new ProductResponse
                {
                    Id = 1,
                    Name = "Laptop",
                    Price = 1200.50m,
                    Stock = 25,
                    Category = categories[0],
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
                    Category = categories[1],
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
                    Category = categories[1],
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
                    Category = categories[0],
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
                    Category = categories[1],
                    CreatedAt = DateTime.UtcNow.AddDays(-12),
                    IsActive = true
                },
                new ProductResponse
                {
                    Id = 6,
                    Name = "Webcam HD",
                    Price = 79.99m,
                    Stock = 30,
                    Category = categories[0],
                    CreatedAt = DateTime.UtcNow.AddDays(-10),
                    IsActive = true
                },
                new ProductResponse
                {
                    Id = 7,
                    Name = "USB-C Hub",
                    Price = 49.99m,
                    Stock = 60,
                    Category = categories[1],
                    CreatedAt = DateTime.UtcNow.AddDays(-8),
                    IsActive = true
                },
                new ProductResponse
                {
                    Id = 8,
                    Name = "External SSD",
                    Price = 149.99m,
                    Stock = 20,
                    Category = categories[2],
                    CreatedAt = DateTime.UtcNow.AddDays(-5),
                    IsActive = true
                }
            };
        }
    }
}
