using FragranceApi.BLL.Interfaces;
using FragranceApi.DTOs.Products;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace FragranceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IValidator<CreateProductDto> _createValidator;
        private readonly IValidator<UpdateProductDto> _updateValidator;

        public ProductsController(IProductService service,
             IValidator<CreateProductDto> createValidator,
             IValidator<UpdateProductDto> updateValidator)
        {
            _service = service;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            string? name, decimal? minPrice,  decimal? maxPrice, int? minStock, string? sortBy, bool sortDescending = false,
            int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1 || pageSize <= 10)
                return BadRequest("Invalid page number or page size.");
            return Ok(await _service.GetAllAsync(
                name,
                minPrice,
                maxPrice,
                minStock,
                sortBy,
                sortDescending,
                pageNumber,
                pageSize));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var product = await _service.CreateAsync(dto);

            return Created("/api/products", product);
        }
    }
}
