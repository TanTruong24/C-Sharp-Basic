using CachingSample.Data;
using CachingSample.Models;
using InMemoryCaching.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CachingSample.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly ICacheService _cache;
        private readonly ILogger<ProductService> _logger;
        private const string ProductCacheKey = "products_cache";

        public ProductService(AppDbContext context, ICacheService cache, ILogger<ProductService> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var stopwatch = Stopwatch.StartNew();

            bool fromCache = true;
            List<Product>? products = await _cache.GetAsync<List<Product>>(ProductCacheKey);

            if (products == null || products.Count == 0)
            {
                fromCache = false;

                products = await _context.Products.AsNoTracking().ToListAsync();

                await _cache.SetAsync(ProductCacheKey, products, TimeSpan.FromMinutes(5));

                _logger.LogInformation("Cache set: {key}", ProductCacheKey);
            }
            else
            {
                _logger.LogInformation("Cache hit: {key}", ProductCacheKey);
            }

            stopwatch.Stop();
            _logger.LogInformation("[CACHE] Used cache: {fromCache}", fromCache);
            _logger.LogInformation("[PERF] Execution time: {elapsed} ms", stopwatch.ElapsedMilliseconds);

            return products;
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
