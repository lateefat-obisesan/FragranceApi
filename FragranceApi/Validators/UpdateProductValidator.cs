using FluentValidation;
using FragranceApi.DTOs.Products;

namespace FragranceApi.Validators
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0);
        }
    }
}
