using CachingSample.Data;
using CachingSample.Models;
using InMemoryCaching.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.Json;

namespace CachingSample.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IDistributedCache _cache;
        private readonly ILogger<ProductService> _logger;
        private const string ProductCacheKey = "products_cache";

        public ProductService(AppDbContext context, IDistributedCache cache, ILogger<ProductService> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            List<Product>? products = null;
            bool fromCache = true;

            var cached = await _cache.GetStringAsync(ProductCacheKey);
            _logger.LogInformation("✅ Redis cache saved: {key}", ProductCacheKey);
            if (!string.IsNullOrEmpty(cached))
            {
                products = JsonSerializer.Deserialize<List<Product>>(cached);
            }
            else
            {
                fromCache = false;
                products = await _context.Products.AsNoTracking().ToListAsync();

                var serialized = JsonSerializer.Serialize(products);

                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                };

                await _cache.SetStringAsync(ProductCacheKey, serialized, options);
            }

            stopwatch.Stop();
            _logger.LogInformation("[CACHE] Used Redis: {fromCache}", fromCache);
            _logger.LogInformation("[PERF] Execution time: {elapsed} ms", stopwatch.ElapsedMilliseconds);

            return products!;
        }

        public async Task<Product> CreateAsync(ProductCreationDto dto)
        {
            var product = new Product(dto.Name, dto.Description, dto.Price);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            await _cache.RemoveAsync(ProductCacheKey); // Invalidate cache

            _logger.LogInformation("[CACHE] Removed Redis key: {key}", ProductCacheKey);

            return product;
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
