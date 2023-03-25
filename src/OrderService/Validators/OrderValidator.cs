using FluentValidation;
using OrderService.Models;

namespace OrderService.Validators
{
    public class OrderValidator : AbstractValidator<OrderModel>
    {
        public OrderValidator()
        {
            RuleFor(x => x.AddressId).NotEmpty();
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.Price).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty().GreaterThanOrEqualTo(1);
            RuleFor(x => x.Status).NotEmpty();
        }
    }
}
