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
    }

}
