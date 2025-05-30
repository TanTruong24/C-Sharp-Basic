using CachingSample.Models;
using InMemoryCaching.Models;

namespace CachingSample.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> CreateAsync(ProductCreationDto dto);

        Task<Product?> GetByIdAsync(Guid id);
    }
}
