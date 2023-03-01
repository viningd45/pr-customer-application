using CustomerApplication.DataAccess;
using CustomerApplication.Models.DatabaseModel;
using CustomerApplication.Models.RequestModel;
using CustomerApplication.Helpers.Extensions;
using FluentValidation;
using FluentValidation.Results;

namespace CustomerApplication.Logic;

public class CustomerHoursLogic : ICustomerHoursLogic
{
    private readonly ICustomerHoursData _data;
    private readonly IValidator<CustomerHoursRequest> _validator;

    public CustomerHoursLogic(ICustomerHoursData data, IValidator<CustomerHoursRequest> validator) 
    { 
        _data = data;
        _validator = validator;
    }

    public bool CreateCustomerHours(CustomerHoursRequest customerHours, out ValidationResult validationResult)
    {
        validationResult = _validator.Validate(customerHours);
        if (!validationResult.IsValid) return false;

        customerHours.FormatHours();

        this.AdjustAndCleanOverlaps(ref customerHours);

        return _data.CreateCustomerHours(new CustomerHours(customerHours)) > 0;
    }

    public bool DeleteCustomerHours(int customerOpenHourId)
    {
        return _data.DeleteCustomerHours(customerOpenHourId) > 0;
    }

    public bool DeleteCustomerHours(IEnumerable<CustomerHoursRequest> hours)
    {
        bool ret = true;

        foreach (var hour in hours)
        {
            if (this.DeleteCustomerHours(hour.Id) == false) ret = false;
        }

        return ret;
    }

    public IEnumerable<CustomerHoursRequest> GetCustomerHours(int customerId)
    {
        return _data.GetCustomerHours(customerId).Select(hours => new CustomerHoursRequest(hours));
    }

    public IEnumerable<CustomerHoursRequest> GetOverlappingCustomerHours(CustomerHoursRequest customerHours)
    {
        TimeSpan start = customerHours.OpeningFormatted.TimeOfDay;
        TimeSpan end = customerHours.ClosingFormatted.TimeOfDay;

        return _data.GetCustomerHours(customerHours.CustomerId)
            .Where(hours =>
            (start.Between(hours.Opening, hours.Closing) || end.Between(hours.Opening, hours.Closing) || hours.Opening.Between(start, end) || hours.Closing.Between(start, end))
                    && hours.DayOfWeek == customerHours.DayOfWeek
                    && hours.Id != customerHours.Id)
            .Select(hours => new CustomerHoursRequest(hours));
    }

    public bool UpdateCustomerHours(CustomerHoursRequest customerHours, out ValidationResult validationResult)
    {
        validationResult = _validator.Validate(customerHours);
        if (!validationResult.IsValid) return false;

        customerHours.FormatHours();

        this.AdjustAndCleanOverlaps(ref customerHours);

        return _data.UpdateCustomerHours(new CustomerHours(customerHours)) > 0;
    }

    public bool DeleteAllCustomerHours(int customerId)
    {
        IEnumerable<CustomerHoursRequest> openings = this.GetCustomerHours(customerId);

        int modified = 0;

        foreach (var opening in openings)
            modified += _data.DeleteCustomerHours(opening.Id);

        return modified == openings.ToList().Count;
    }

    private void AdjustAndCleanOverlaps(ref CustomerHoursRequest hours)
    {
        IList<CustomerHoursRequest> overlaps = this.GetOverlappingCustomerHours(hours).ToList();

        if (overlaps.Count > 0)
        {
            this.AdjustOverlappedHours(ref hours, overlaps);

            int customerHoursId = hours.Id;
            this.DeleteCustomerHours(overlaps.Where(over => over.Id != customerHoursId));
        }
    }

    public void AdjustOverlappedHours (ref CustomerHoursRequest customerHours, IList<CustomerHoursRequest> overlappingHours)
    {
        overlappingHours.Add(customerHours);
        customerHours.OpeningFormatted = overlappingHours.Min(h => DateTime.Today.Add(h.OpeningFormatted.TimeOfDay));
        customerHours.ClosingFormatted = overlappingHours.Max(h => DateTime.Today.Add(h.ClosingFormatted.TimeOfDay));
    }
}
