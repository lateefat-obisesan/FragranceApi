using FragranceApi.BLL.Interfaces;
using FragranceApi.DTOs.Orders;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace FragranceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;
        private readonly IValidator<CreateOrderDto> _validator;

        public OrdersController(
          IOrderService service,
          IValidator<CreateOrderDto> validator)
        {
            _service = service;
            _validator = validator;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _service.GetByIdAsync(id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderDto dto)
        {
            var order = await _service.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = order.Id },
                order);
        }
    }
}
