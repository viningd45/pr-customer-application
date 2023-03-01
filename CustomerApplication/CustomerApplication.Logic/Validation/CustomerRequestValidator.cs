using CustomerApplication.Models.RequestModel;
using CustomerApplication.Helpers.Extensions;
using FluentValidation;

namespace CustomerApplication.Logic.Validation;

public class CustomerRequestValidator : AbstractValidator<CustomerRequest>
{
    public CustomerRequestValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        RuleFor(customer => customer.Name).NotNull().NotEmpty().WithMessage("Customer name is required").MaximumLength(70).WithMessage("Name cannot be more than 70 characters"); ;
        RuleFor(customer => customer.PhoneNumber).NotNull().NotEmpty().WithMessage("Customer phone number is required").MinimumLength(10).MaximumLength(14).WithMessage("A valid 10 digit phone number is required").Custom((value, context) =>
        {
            string parsed = value.GetNumbers();
            if (parsed.Length != 10) context.AddFailure("Phone number may only contain numbers");
        });
        RuleFor(customer => customer.AddressLineOne).NotNull().NotEmpty().WithMessage("Customer address is required").MaximumLength(35).WithMessage("Address cannot be more than 35 characters");
        RuleFor(customer => customer.AddressLineTwo).MaximumLength(35).WithMessage("Address line two cannot be more than 35 characters");
        RuleFor(customer => customer.City).NotNull().NotEmpty().WithMessage("Customer city is required").MaximumLength(35).WithMessage("City cannot be more than 35 characters");
        RuleFor(customer => customer.State).NotNull().NotEmpty().WithMessage("Customer state is required").MaximumLength(35).WithMessage("State cannot be more than 35 characters");
        RuleFor(customer => customer.ZipCode).NotNull().NotEmpty().WithMessage("Customer zip code is required").Custom((value, context) =>
        {
            if (value.Length < 5)
            {
                context.AddFailure("Customer zip code needs at least 5 numbers");
                return;
            }
            if(value.Length > 5)
            {
                var split = value.Split('-');
                if(split.Length == 2)
                {
                    string parsed = split[0].GetNumbers();
                    if (parsed.Length != split[0].Length || parsed.Length != 5)
                    {
                        context.AddFailure("Customer zip code needs at least 5 numbers");
                        return;
                    }
                    parsed = split[1].GetNumbers();
                    if (parsed.Length != split[1].Length || parsed.Length != 4)
                    {
                        context.AddFailure("Customer zip code may only be 5 or 9 digits long");
                        return;
                    }
                }
                else
                {
                    context.AddFailure("Customer zip code is not a valid zip code");
                }
            }
            else
            {
                string parsed = value.GetNumbers();
                if (parsed.Length != value.Length) context.AddFailure("Customer zip code may only contain numbers");
            }
        });
    }
}
