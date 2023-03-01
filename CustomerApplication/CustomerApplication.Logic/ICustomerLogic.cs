using CustomerApplication.Models.RequestModel;
using FluentValidation.Results;

namespace CustomerApplication.Logic;

public interface ICustomerLogic
{
    IEnumerable<CustomerRequest> GetCustomers(CustomerRequest filter);
    CustomerRequest GetCustomer(int id);
    bool CreateCustomer(CustomerRequest customer, out ValidationResult validationResult);
    bool UpdateCustomer(CustomerRequest customer, out ValidationResult validationResult);
    bool DeleteCustomer(int customerId);
}
