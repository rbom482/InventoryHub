using System.Text.Json;

namespace ClientApp.Services
{
    public interface IApiService
    {
        Task<ApiResponse<T>?> GetAsync<T>(string endpoint);
        Task<T?> GetDirectAsync<T>(string endpoint);
    }

    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiService> _logger;

        public ApiService(HttpClient httpClient, ILogger<ApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ApiResponse<T>?> GetAsync<T>(string endpoint)
        {
            try
            {
                _logger.LogInformation($"Making API request to: {endpoint}");
                var response = await _httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiResponse<T>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                _logger.LogInformation($"API request successful: {endpoint}");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"API request failed: {endpoint}");
                throw;
            }
        }

        public async Task<T?> GetDirectAsync<T>(string endpoint)
        {
            try
            {
                _logger.LogInformation($"Making direct API request to: {endpoint}");
                var response = await _httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                _logger.LogInformation($"Direct API request successful: {endpoint}");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Direct API request failed: {endpoint}");
                throw;
            }
        }
    }

    // Shared models to avoid duplication
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public Category Category { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public DateTime Timestamp { get; set; }
        public int TotalCount { get; set; }
    }

    // Product response models for deserialization
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public CategoryResponse Category { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }

    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
