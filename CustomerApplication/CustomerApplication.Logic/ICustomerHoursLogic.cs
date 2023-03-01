using CustomerApplication.Models.RequestModel;
using FluentValidation.Results;

namespace CustomerApplication.Logic;

public interface ICustomerHoursLogic
{
    IEnumerable<CustomerHoursRequest> GetCustomerHours(int customerId);
    IEnumerable<CustomerHoursRequest> GetOverlappingCustomerHours(CustomerHoursRequest customerHours);
    bool CreateCustomerHours(CustomerHoursRequest opening, out ValidationResult validationResult);
    bool UpdateCustomerHours(CustomerHoursRequest opening, out ValidationResult validationResult);
    bool DeleteCustomerHours(int customerOpenHourId);
    bool DeleteAllCustomerHours(int customerId);
}
