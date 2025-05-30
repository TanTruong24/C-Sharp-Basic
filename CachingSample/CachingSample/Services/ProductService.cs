using CachingSample.Data;
using CachingSample.Models;
using InMemoryCaching.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace CachingSample.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMemoryCache _cache;
        private const string ProductCacheKey = "products_cache";
        private readonly ILogger<ProductService> _logger;

        public ProductService(AppDbContext context, IMemoryCache cache, ILogger<ProductService> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            List<Product>? products;
            bool fromCache = true;

            if (!_cache.TryGetValue(ProductCacheKey, out products))
            {
                fromCache = false;
                products = await _context.Products.AsNoTracking().ToListAsync();

                var options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(30));

                _cache.Set(ProductCacheKey, products, options);
            }

            stopwatch.Stop();

            _logger.LogInformation("[CACHE] Used: {fromCache}", fromCache);
            _logger.LogInformation("[PERF] Execution time: {elapsed} ms", stopwatch.ElapsedMilliseconds);

            return products!;
        }

        public async Task<Product> CreateAsync(ProductCreationDto dto)
        {
            var product = new Product(dto.Name, dto.Description, dto.Price);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            _cache.Remove(ProductCacheKey); // Xóa cache để reload dữ liệu mới lần sau

            return product;
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            // Có thể dùng cache riêng nếu bạn cần cache từng product
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }
    }

}
