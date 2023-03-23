using CustomerService.Models;
using FluentValidation;

namespace CustomerService.Validators
{
    public class CustomerValidator : AbstractValidator<CustomerModel>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.AddressId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
