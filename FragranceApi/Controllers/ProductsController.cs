using FragranceApi.BLL.Interfaces;
using FragranceApi.DTOs.Products;
using Microsoft.AspNetCore.Mvc;

namespace FragranceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ProductQueryDto query)
        {
            return Ok(await _service.GetAllAsync(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            var product = await _service.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = product.Id },
                product);
        }

        [HttpPatch("{id}/stock")]
        public async Task<IActionResult> UpdateStock(
            int id,
            UpdateProductDto dto)
        {
            var updated = await _service.UpdateStockAsync(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }
    }
}
