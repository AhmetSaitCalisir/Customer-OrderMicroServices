using FluentValidation;
using OrderService.Models;

namespace OrderService.Validators
{
    public class OrderValidator : AbstractValidator<OrderModel>
    {
        public OrderValidator()
        {
            RuleFor(x => x.AddressId).NotNull();
            RuleFor(x => x.CustomerId).NotNull();
            RuleFor(x => x.Price).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(x => x.ProductId).NotNull();
            RuleFor(x => x.Quantity).NotNull().GreaterThanOrEqualTo(1);
            RuleFor(x => x.Status).NotNull();
        }
    }
}
