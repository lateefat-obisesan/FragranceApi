using FluentValidation;
using FragranceApi.DTOs.Orders;

namespace FragranceApi.Validators
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0);

            RuleFor(x => x.Items)
                .NotEmpty();

            RuleForEach(x => x.Items)
                .ChildRules(item =>
                {
                    item.RuleFor(x => x.ProductId)
                        .GreaterThan(0);

                    item.RuleFor(x => x.Quantity)
                        .GreaterThan(0);
                });
        }
    }
}
