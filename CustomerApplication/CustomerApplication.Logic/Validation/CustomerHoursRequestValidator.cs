using CustomerApplication.Models.RequestModel;
using FluentValidation;

namespace CustomerApplication.Logic.Validation;

public class CustomerHoursRequestValidator : AbstractValidator<CustomerHoursRequest>
{
    public CustomerHoursRequestValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        RuleFor(hours => hours.CustomerId).GreaterThan(0).WithMessage("A validator customer ID must be provided");
        RuleFor(hours => hours.DayOfWeek).NotNull().WithMessage("A day of the week must be selected");
        RuleFor(hours => hours.Opening).Custom((value, context) =>
        {
            if (DateTime.TryParse(value, out DateTime result) == false) context.AddFailure("A valid time is required for opening time");
        });
        RuleFor(hours => hours.Closing).Custom((value, context) =>
        {
            if (DateTime.TryParse(value, out DateTime result) == false) context.AddFailure("A valid time is required for closing time");
        })
            .Must((self, closingString) => 
            {
                // enforce closing time cannot be earlier than opening time
                DateTime.TryParse(self.Opening, out DateTime opening);
                DateTime.TryParse(closingString, out DateTime closing);

                return opening.TimeOfDay < closing.TimeOfDay; 
            }).WithMessage("Opening time cannot be later than closing time");
    }
}
