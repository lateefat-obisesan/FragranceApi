using FluentValidation;
using FragranceApi.BLL.Interfaces;
using FragranceApi.DTOs.Customers;
using Microsoft.AspNetCore.Mvc;

namespace FragranceApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;
        private readonly IValidator<CreateCustomerDto> _createValidator;
        private readonly IValidator<UpdateCustomerDto> _updateValidator;

        public CustomersController(
            ICustomerService service,
            IValidator<CreateCustomerDto> createValidator,
            IValidator<UpdateCustomerDto> updateValidator)
        {
            _service = service;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _service.GetByIdAsync(id);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerDto dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var customer = await _service.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = customer.Id },
                customer);
        }
    }
}
