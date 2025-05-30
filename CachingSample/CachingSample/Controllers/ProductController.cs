using CachingSample.Services;
using InMemoryCaching.Models;
using Microsoft.AspNetCore.Mvc;

namespace CachingSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetAllAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreationDto dto)
        {
            var product = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product is null)
                return NotFound();

            return Ok(product);
        }
    }

}
