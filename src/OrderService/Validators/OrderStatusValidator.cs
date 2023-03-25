using FluentValidation;
using OrderService.Models;

namespace OrderService.Validators
{
    public class OrderStatusValidator : AbstractValidator<OrderStatusModel>
    {
        public OrderStatusValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Status).NotEmpty();
        }
    }
}
