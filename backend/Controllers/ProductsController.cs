using Microsoft.AspNetCore.Mvc;

namespace FullStackApp.Backend.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            // Fixed JSON structure with correct property names
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop", Price = 999.99, Stock = 15 },
                new Product { Id = 2, Name = "Mouse", Price = 29.99, Stock = 50 },
                new Product { Id = 3, Name = "Keyboard", Price = 79.99, Stock = 25 },
                new Product { Id = 4, Name = "Monitor", Price = 299.99, Stock = 8 },
                new Product { Id = 5, Name = "Headphones", Price = 149.99, Stock = 20 },
                new Product { Id = 6, Name = "Webcam", Price = 89.99, Stock = 12 },
                new Product { Id = 7, Name = "USB Cable", Price = 12.99, Stock = 100 },
                new Product { Id = 8, Name = "External Hard Drive", Price = 119.99, Stock = 18 }
            };

            return Ok(products);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            // Sample product data - replace with actual database call
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop", Price = 999.99, Stock = 15 },
                new Product { Id = 2, Name = "Mouse", Price = 29.99, Stock = 50 },
                new Product { Id = 3, Name = "Keyboard", Price = 79.99, Stock = 25 },
                new Product { Id = 4, Name = "Monitor", Price = 299.99, Stock = 8 },
                new Product { Id = 5, Name = "Headphones", Price = 149.99, Stock = 20 },
                new Product { Id = 6, Name = "Webcam", Price = 89.99, Stock = 12 },
                new Product { Id = 7, Name = "USB Cable", Price = 12.99, Stock = 100 },
                new Product { Id = 8, Name = "External Hard Drive", Price = 119.99, Stock = 18 }
            };

            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Stock { get; set; }
    }
}
